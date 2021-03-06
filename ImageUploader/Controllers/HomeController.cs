using ImageFileResizer;
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
        private IImageManipulator _imageManipulator;

        public HomeController(IImageManipulator imageManipulator)
        {
            _imageManipulator = imageManipulator;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ImageViewModel model = new ImageViewModel();
            model.FileInfos = new DirectoryInfo(Server.MapPath("~/images")).GetFiles();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase profileFile)
        {
            if (profileFile != null)
            {
                string pic = System.IO.Path.GetFileName(profileFile.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/tempimages"), pic);
                profileFile.SaveAs(path);
                string targetPath = System.IO.Path.Combine(Server.MapPath("~/images"), pic);

                _imageManipulator.ResizeImageBytes(path, targetPath);
            }
            return RedirectToAction("Index");
        }
    }
}