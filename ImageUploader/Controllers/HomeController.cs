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

        [HttpPost]
        public ActionResult ProcessImage(string base64image)
        {
            if (string.IsNullOrEmpty(base64image))
                return RedirectToAction("Index"); ;

            var imageString = base64image.Substring(22); //remove data:image/png;base64,

            byte[] bytes = Convert.FromBase64String(imageString);

            MemoryStream byteStream = new MemoryStream(bytes);

            string targetPath = System.IO.Path.Combine(Server.MapPath("~/images"), "uploadedpic.png");

            _imageManipulator.ResizeImageBytes(byteStream, targetPath);

            return RedirectToAction("Index");
        }
    }
}