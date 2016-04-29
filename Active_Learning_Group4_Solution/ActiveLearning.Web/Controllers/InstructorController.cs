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
using System.Threading.Tasks;

namespace ActiveLearning.Web.Controllers
{
    public class InstructorController : BaseController
    {
        // GET: Instructor
        #region Index
        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Instructor_Code)]
        public ActionResult Index()
        {
            if (!IsUserAuthenticated())
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
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            using (var courseManager = new CourseManager())
            {
                var courseList = courseManager.GetEnrolledCoursesByInstructorSid(GetLoginUser().Instructors.FirstOrDefault().Sid, out message);
                if (courseList == null || courseList.Count() == 0)
                {
                    SetError(message);
                }
                return View(courseList);
            }
        }
        #endregion

        #region Chat
        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Instructor_Code)]
        public ActionResult Chat(int courseSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }
            var claims = new List<Claim>();
            try
            {
                claims.Add(new Claim(ClaimTypes.GroupSid, courseSid.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, GetLoginUser().FullName));

                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties()
                {
                    //ExpiresUtc = DateTime.UtcNow.(200),
                    IsPersistent = true
                }, identity);
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                SetError(Business.Common.Constants.OperationFailedDuringRetrievingValue(Business.Common.Constants.Chat));
            }
            return View(courseSid);
        }

        #endregion

        #region Content
        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Instructor_Code)]
        [HttpGet]
        public ActionResult ManageContent(int courseSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }

            var items = new List<Content>();
            using (var contentManager = new ContentManager())
            {
                message = string.Empty;
                var contents = contentManager.GetContentsByCourseSid(courseSid, out message);
                if (contents != null)
                {
                    items = contents.ToList();
                }
                else
                {
                    SetError(message);
                    return View(items);
                }
            }
            ViewBag.CourseSid = courseSid;
            GetErrorAneMessage();
            return View(items);
        }

        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Instructor_Code)]
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, int courseSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }
            //if (Request.UrlReferrer == null)
            //{
            //    return RedirectToError(Business.Common.Constants.ValueIsEmpty("UrlReferrer"));
            //}

            using (var contentManger = new ContentManager())
            {
                var content = contentManger.AddContent(this, file, courseSid, out message);
                if (content != null)
                {
                    SetMessage(Business.Common.Constants.ValueSuccessfuly("File has been uploaded"));
                }
                else
                {
                    SetError(message);
                }
            }
            //return new RedirectResult(Request.UrlReferrer.ToString());
            return RedirectToAction("ManageContent", new { courseSid = courseSid });
        }


        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Instructor_Code)]
        public ActionResult Delete(int courseSid, int contentSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }
            //if (Request.UrlReferrer == null)
            //{
            //    return RedirectToError(Business.Common.Constants.ValueIsEmpty("UrlReferrer"));
            //}
            using (var contentManager = new ContentManager())
            {
                if (contentManager.DeleteContent(this, contentSid, out message))
                {
                    SetMessage(Business.Common.Constants.ValueSuccessfuly("File hase been deleted"));
                }
                else
                {
                    SetError(message);
                }
            }
            return RedirectToAction("ManageContent", new { courseSid = courseSid });
        }

        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Instructor_Code)]
        public ActionResult Download(int courseSid, int contentSid, string originalFileName)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }
            //if (Request.UrlReferrer == null)
            //{
            //    return RedirectToError(Business.Common.Constants.ValueIsEmpty("UrlReferrer"));
            //}
            string filepath;
            string fileType;
            using (var contentManager = new ContentManager())
            {
                var content = contentManager.GetContentByContentSid(contentSid, out message);
                if (content == null)
                {
                    SetError(message);
                    return RedirectToAction("ManageContent", new { courseSid = courseSid });
                    //return RedirectToError(message);
                }
                filepath = content.Path + content.FileName;
                fileType = content.Type;
            }
            var file = File(filepath, System.Net.Mime.MediaTypeNames.Application.Octet, originalFileName);
            if (file == null)
            {
                SetError(ActiveLearning.Business.Common.Constants.ValueNotFound(ActiveLearning.Business.Common.Constants.File));
                return RedirectToAction("ManageContent", new { courseSid = courseSid });
                //return RedirectToError());
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
            return RedirectToError(null); ;
        }
        #endregion

        #region Quiz

        // GET: ManageQuiz
        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Instructor_Code)]
        public ActionResult ManageQuiz(int courseSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }

            using (var quizManager = new QuizManager())
            {
                var listQuiz = quizManager.GetActiveQuizQuestionsByCourseSid(courseSid, out message);
                if (listQuiz == null)
                {
                    SetError(message);
                }
                TempData["cid"] = courseSid;
                TempData.Keep("cid");
                //TempData.Peek("cid");
                return View(listQuiz);
            }

        }

        // GET: ManageQuiz/CreateQuizQuestion
        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Instructor_Code)]
        public ActionResult CreateQuizQuestion()
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            return View();
        }

        // POST: ManageQuiz/CreateQuizQuestion
        [HttpPost]
        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Instructor_Code)]
        public ActionResult CreateQuizQuestion(QuizQuestion quizQuestion)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            int cid = Convert.ToInt32(TempData["cid"]);

            string message = string.Empty;
            if (!HasAccessToCourse(cid, out message))
            {
                return RedirectToError(message);
            }

            using (var quizManager = new QuizManager())
            {
                if (quizManager.AddQuizQuestionToCourse(quizQuestion, cid, out message) == null)
                {
                    SetError(message);
                    return View();
                }
            }
            SetMessage(Business.Common.Constants.ValueIsSuccessful("Quiz Question has been created"));
            return RedirectToAction("ManageQuiz", new { id = cid });
        }


        // GET: ManageQuiz/DeleteQuizQuestion/6
        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Instructor_Code)]
        public ActionResult DeleteQuizQuestion(int courseSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }

            using (var quizManager = new QuizManager())
            {
                QuizQuestion quizQuesion = quizManager.GetQuizQuestionByQuizQuestionSid(courseSid, out message);
                if (quizQuesion == null)
                {
                    SetError(message);
                }
                return View(quizQuesion);
            }
        }

        // POST: ManageQuiz/DeleteQuizQuestion/6
        [HttpPost, ActionName("DeleteQuizQuestion")]
        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Instructor_Code)]
        public ActionResult DeleteQuiz(int id)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            int cid = Convert.ToInt32(TempData["cid"]);
            string message = string.Empty;
            using (var deleteQuiz = new QuizManager())
            {
                QuizQuestion quizQuestion = deleteQuiz.GetQuizQuestionByQuizQuestionSid(id, out message);
                if (deleteQuiz.DeleteQuizQuestion(quizQuestion, out message))
                {
                    SetMessage(Business.Common.Constants.ValueIsSuccessful("Quiz Question has been deleted"));
                }
                else
                {
                    SetError(message);
                }
                return RedirectToAction("ManageQuiz", new { id = cid });
            };
        }

        // GET: ManageQuiz/EditQuizQuestio/6
        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Instructor_Code)]
        public ActionResult EditQuizQuestion(int courseSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }

            using (var getQuizQuestion = new QuizManager())
            {
                QuizQuestion quizQuesion = getQuizQuestion.GetQuizQuestionByQuizQuestionSid(courseSid, out message);
                if (quizQuesion == null)
                {
                    SetError(message);
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
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            int courseSid = Convert.ToInt32(TempData["cid"]);
            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }

            var quizQusToUpdate = TempData["QuizQuesion"] as QuizQuestion;
            quizQusToUpdate.Title = quizQuestion.Title;

            using (var updateQus = new QuizManager())
            {
                if (updateQus.UpdateQuizQuestion(quizQusToUpdate, out message))
                {
                    SetMessage(Business.Common.Constants.ValueIsSuccessful("Quiz Question has been updated"));

                }
                else
                {
                    SetError(message);
                }
                return RedirectToAction("ManageQuiz", new { id = courseSid });
            }
        }

        public async Task<ActionResult> QuizStatistics(int courseSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }
            using (var quizManager = new QuizManager())
            {
                return View(await quizManager.GenerateStatistics(courseSid));
            }
        }

        #endregion
    }
}