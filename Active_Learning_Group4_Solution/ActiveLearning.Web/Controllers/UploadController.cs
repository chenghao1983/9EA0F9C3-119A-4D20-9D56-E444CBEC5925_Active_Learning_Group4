using ActiveLearning.Business.Implementation;
using ActiveLearning.Business.Interface;
using ActiveLearning.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActiveLearning.Web.Controllers
{
    public class UploadController : BaseController
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {

                    var fileName = Path.GetFileName(file.FileName);
                    Content fileDetail = new Content()
                    {
                        //TODO set the courseSID
                        CourseSid = 2,
                        Path = "~/App_Data/Upload/",
                        OriginalFileName = fileName,
                        FileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName),
                        Type = "",
                        CreateDT = DateTime.Now
                    };

                    // Write to Database
                    IFileManager fileManager = new FileManager();
                    fileManager.AddFile(fileDetail);


                    var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), fileDetail.FileName + Path.GetExtension(fileName));
                    file.SaveAs(path);
                }
                ViewBag.Message = "Upload successful";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Upload failed";
                return View();
            }
        }
    }
}