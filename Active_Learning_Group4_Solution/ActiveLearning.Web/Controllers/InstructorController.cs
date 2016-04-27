using ActiveLearning.DB;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using ActiveLearning.Business.Common;
using ActiveLearning.Business.Implementation;
using ActiveLearning.Business.Interface;
using ActiveLearning.DB;
using ActiveLearning.Web.Filter;

namespace ActiveLearning.Web.Controllers
{
    public class InstructorController : BaseController
    {
        // GET: Instructor
        #region Index
        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Instructor_Code)]
        public ActionResult Index()
        {
            if (GetLoginUser() == null)
            {
                return RedirectToLogin();
            }
            return View();
        }
        #endregion

        #region Course
        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Instructor_Code)]
        public ActionResult CourseList()
        {
            if (GetLoginUser() == null)
            {
                return RedirectToLogin();
            }

            string message = string.Empty;
            using (var courseManager = new CourseManager())
            {
                var courseList = courseManager.GetEnrolledCoursesByInstructorSid(GetLoginUser().Instructors.FirstOrDefault().Sid, out message);
                return View(courseList);
            }
        }
        #endregion

        #region Chat
        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Instructor_Code)]
        public ActionResult Chat(int courseSid)
        {
            if (GetLoginUser() == null)
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }
            var claims = new List<Claim>();


            claims.Add(new Claim(ClaimTypes.GroupSid, courseSid.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, GetLoginUser().FullName));

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties()
            {
                //ExpiresUtc = DateTime.UtcNow.(200),
                IsPersistent = true
            }, identity);

            return View(courseSid);
        }

        #endregion

        #region Content
        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Instructor_Code)]
        [HttpGet]
        public ActionResult ManageContent(int courseSid)
        {
            if (GetLoginUser() == null)
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }

            List<Content> items = new List<Content>();
            using (var contentManager = new ContentManager())
            {
                message = string.Empty;
                var contents = contentManager.GetContentsByCourseSid(courseSid, out message);
                if (contents != null)
                {
                    items = contents.ToList();
                }
            }
            ViewBag.CourseSid = courseSid;
            ViewBag.Message = TempData["message"];
            return View(items);
        }

        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Instructor_Code)]
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, int courseSid)
        {

            string message = string.Empty;
            using (var contentManger = new ContentManager())
            {
                var content = contentManger.AddContent(this, file, courseSid, out message);
                if (content != null)
                {
                    TempData["message"] = Business.Common.Constants.ValueIsSuccessful(Business.Common.Constants.Upload);
                    return new RedirectResult(Request.UrlReferrer.ToString());
                }
                else
                {
                    TempData["message"] = message;
                    return new RedirectResult(Request.UrlReferrer.ToString());
                }

            }
        }
        //try
        //{

        //}

        //        if (file.ContentLength > 0)
        //        {
        //            string guid = Guid.NewGuid().ToString();

        //            var fileName = Path.GetFileName(file.FileName);
        //            Content fileDetail = new Content()
        //            {
        //                //TODO set the courseSID
        //                CourseSid = 2,
        //                Path = "~/App_Data/Upload/",
        //                OriginalFileName = fileName,
        //                FileName = guid + Path.GetExtension(fileName),
        //                Type = "",
        //                CreateDT = DateTime.Now
        //            };

        //            // Write to Database
        //            using (var fileManager = new ContentManager())
        //            {
        //                string message = string.Empty;
        //                fileManager.AddContent(this, file, 1, out message);
        //            }

        //            var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), guid + Path.GetExtension(fileName));
        //            file.SaveAs(path);
        //        }
        //    ViewBag.Message = "Upload successful";
        //    return View();
        //}
        //catch (Exception ex)
        //{
        //    ViewBag.Message = "Upload failed. " + ex.Message;


        //}
        //}

        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Instructor_Code)]
        public ActionResult Download(int courseSid, int contentSid, string originalFileName)
        {
            if (GetLoginUser() == null)
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }
            string filepath;
            string fileType;
            using (var contentManager = new ContentManager())
            {
                var content = contentManager.GetContentByContentSid(contentSid, out message);
                if (content == null)
                {
                    return RedirectToError(message);
                }
                filepath = content.Path + content.FileName;
                fileType = content.Type;
            }
            var file = File(filepath, System.Net.Mime.MediaTypeNames.Application.Octet, originalFileName);
            if (file == null)
            {
                return RedirectToError(ActiveLearning.Business.Common.Constants.ValueNotFound(ActiveLearning.Business.Common.Constants.File));
            }
            if (fileType.Equals(ActiveLearning.Business.Common.Constants.Content_Type_Video))
            {

                ViewBag.VideoPath = filepath;
                return View("Video");
            }
            else if (fileType.Equals(ActiveLearning.Business.Common.Constants.Content_Type_File))
            {
                return file;
            }
            return null;
        }
        #endregion

        #region Quiz

        // GET: ManageQuiz
        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Instructor_Code)]
        public ActionResult ManageQuiz(int id)
        {
            string message = string.Empty;
            using (var quizManager = new QuizManager())
            {

                var listQuiz = quizManager.GetActiveQuizQuestionsByCourseSid(id, out message);
                if (listQuiz == null)
                {
                    ViewBag.Message = "The List is empty !";
                }
                TempData["cid"] = id;
                TempData.Keep("cid");
                TempData.Peek("cid");
                return View(listQuiz);
            }

        }

        // GET: ManageQuiz/CreateQuizQuestion
        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Instructor_Code)]
        public ActionResult CreateQuizQuestion()
        {
            return View();

        }

        // POST: ManageQuiz/CreateQuizQuestion
        [HttpPost]
        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Instructor_Code)]
        public ActionResult CreateQuizQuestion(QuizQuestion quizQuestion)
        {

            try
            {

                int cid = Convert.ToInt32(TempData["cid"]);

                string message = string.Empty;
                using (var quizManager = new QuizManager())
                {

                    if (quizManager.AddQuizQuestionToCourse(quizQuestion, cid, out message) == null)
                    {
                        ViewBag.Message = message;
                        return View();
                    }
                }
                ViewBag.Message = "Quiz Question Created !";

                return RedirectToAction("ManageQuiz", new { id = cid });
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(quizQuestion);
        }


        // GET: ManageQuiz/DeleteQuizQuestion/6
        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Instructor_Code)]
        public ActionResult DeleteQuizQuestion(int id)
        {
            string message = string.Empty;
            using (var quizManager = new QuizManager())
            {


                QuizQuestion quizQuesion = quizManager.GetQuizQuestionByQuizQuestionSid(id, out message);
                if (quizQuesion == null)
                {
                    return HttpNotFound();
                }
                return View(quizQuesion);
            }

        }


        // POST: ManageQuiz/DeleteQuizQuestion/6
        [HttpPost, ActionName("DeleteQuizQuestion")]
        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Instructor_Code)]
        public ActionResult DeleteQuiz(int id)
        {
            try
            {

                int cid = Convert.ToInt32(TempData["cid"]);
                string message = string.Empty;
                using (var deleteQuiz = new QuizManager())
                {
                    QuizQuestion quizQuestion = deleteQuiz.GetQuizQuestionByQuizQuestionSid(id, out message);
                    if (deleteQuiz.DeleteQuizQuestion(quizQuestion, out message))
                    {
                        return RedirectToAction("ManageQuiz", new { id = cid });
                    }
                    return RedirectToError(message);
                };
            }
            catch (Exception e)
            {
                if (this.HttpContext.IsDebuggingEnabled)
                {
                    ModelState.AddModelError(string.Empty, e.ToString());
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Some technical error happened.");
                }
                return View();
            }

        }

        // GET: ManageQuiz/EditQuizQuestio/6
        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Instructor_Code)]
        public ActionResult EditQuizQuestion(int id)
        {
            string message = string.Empty;
            using (var getQuizQuestion = new QuizManager())
            {
                QuizQuestion quizQuesion = getQuizQuestion.GetQuizQuestionByQuizQuestionSid(id, out message);
                if (quizQuesion == null)
                {
                    return HttpNotFound();
                }
                TempData["QuizQuesion"] = quizQuesion;
                return View(quizQuesion);
            };
        }

        // POST: ManageQuiz/EditQuizQuestio/6
        [HttpPost, ActionName("EditQuizQuestion")]
        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Instructor_Code)]
        public ActionResult updateQuizQus(QuizQuestion quizQuestion)
        {
            try
            {
                int cid = Convert.ToInt32(TempData["cid"]);
                string message = string.Empty;
                var quizQusToUpdate = TempData["QuizQuesion"] as QuizQuestion;
                quizQusToUpdate.Title = quizQuestion.Title;

                using (var updateQus = new QuizManager())
                {
                    if (updateQus.UpdateQuizQuestion(quizQusToUpdate, out message))
                    {
                        return RedirectToAction("ManageQuiz", new { id = cid });
                    }
                    ViewBag.Message = message;
                    return RedirectToError(message);
                }
            }
            catch (Exception e)
            {
                if (this.HttpContext.IsDebuggingEnabled)
                {
                    ModelState.AddModelError(string.Empty, e.ToString());
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Some technical error happened.");
                }
                return View();
            }
        }

        #endregion
    }
}