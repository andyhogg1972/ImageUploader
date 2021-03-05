using ImageUploader.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageUploader.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public ActionResult Index()
        {
            ProfileViewModel model = new ProfileViewModel();
            model.FileInfos = new DirectoryInfo(Server.MapPath("~/images")).GetFiles();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase profileFile)
        {
            if (profileFile != null)
            {
                string pic = System.IO.Path.GetFileName(profileFile.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/images"), pic);
                profileFile.SaveAs(path);
            }
            return RedirectToAction("Index");
        }
    }
}