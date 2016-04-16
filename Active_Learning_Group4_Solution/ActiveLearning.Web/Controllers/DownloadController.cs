using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActiveLearning.Business.Implementation;
using ActiveLearning.Business.Interface;
using ActiveLearning.DB;


namespace ActiveLearning.Web.Controllers
{
    public class DownloadController : BaseController
    {
        // GET: Download
        public ActionResult Index()
        {
            // TODO 
            // Get course ID
            IFileManager fileManager = new FileManager();
            var files = fileManager.GetFiles(2);

            List<string> items = new List<string>();
            foreach (var file in files)
            {
                items.Add(file.OriginalFileName);
            }

            return View(items);
        }

        public FileResult DownloadFile(string FileName)
        {
            // convert filename to GUID filename
            IFileManager fileManager = new FileManager();
            var guidFileName = fileManager.GetGUIDFile(FileName,2);


            return File("~/App_Data/Upload/" + guidFileName, System.Net.Mime.MediaTypeNames.Application.Octet, FileName);
        }
    }
}