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

namespace ActiveLearning.Web.Controllers
{
    public class InstructorController : BaseController
    {
        // GET: Instructor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CourseList()
        {
            string message = string.Empty;
            using (var courseManager = new CourseManager())
            {

                var listCourse = courseManager.GetAllCourses(out message);
                if (listCourse == null || listCourse.Count() == 0)
                {
                    ViewBag.Message = "The List is empty !";
                }

                return View(listCourse);
            }

        }


        public ActionResult Chat(string courseSid)
        {
            var claims = new List<Claim>();

            // TODO: groupid : courseSid parameter, name 
            courseSid = "1";

            claims.Add(new Claim(ClaimTypes.GroupSid, courseSid));
            claims.Add(new Claim(ClaimTypes.Name, GetLoginUser().FullName));

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties()
            {
                ExpiresUtc = DateTime.UtcNow.AddDays(200),
                IsPersistent = true
            }, identity);


            return View();

        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string guid = Guid.NewGuid().ToString();

                    var fileName = Path.GetFileName(file.FileName);
                    Content fileDetail = new Content()
                    {
                        //TODO set the courseSID
                        CourseSid = 2,
                        Path = "~/App_Data/Upload/",
                        OriginalFileName = fileName,
                        FileName = guid + Path.GetExtension(fileName),
                        Type = "",
                        CreateDT = DateTime.Now
                    };

                    // Write to Database
                    using (var fileManager = new ContentManager())
                    {
                        string message = string.Empty;
                        fileManager.AddContent(fileDetail, 1, out message);
                    }

                    var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), guid + Path.GetExtension(fileName));
                    file.SaveAs(path);
                }
                ViewBag.Message = "Upload successful";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Upload failed. " + ex.Message;
                return View();
            }
        }

        #region Quiz

        // GET: ManageQuiz
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
        public ActionResult CreateQuizQuestion()
        {
            return View();

        }

        // POST: ManageQuiz/CreateQuizQuestion
        [HttpPost]
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