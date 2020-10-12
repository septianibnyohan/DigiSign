using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using DigiSign.Helpers;
using Microsoft.AspNetCore.Hosting;

namespace DigiSign.Configs
{
    public class App
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public App(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public dynamic Locale = new
        {
            //Default = System.Web.Configuration.WebConfigurationManager.AppSettings["APP_LOCALE_COLUMN"],
            //Fallback = System.Web.Configuration.WebConfigurationManager.AppSettings["APP_LOCALE_FALLBACK"]
        };

        public string Storage {
            get
            {
                //var dirInfo = new DirectoryInfo(StackHelper.__FILE__);
                //return dirInfo.Parent.FullName.Replace("Configs", "Uploads\\");
                //return HttpContext.Current.Server.MapPath("~/Uploads") + "\\";
                var path = _hostingEnvironment.ContentRootPath + "/Uploads/";
                return path;
            }
        }
            
    }
}