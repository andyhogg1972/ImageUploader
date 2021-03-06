using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ImageUploader.Models
{
    public class ImageViewModel
    {
        public string Image
        {
            get;
            set;
        }

        public FileInfo[] FileInfos
        {
            get; set;
        }
    }
}