using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZSZ.MVC.Models;

namespace ZSZ.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var imgpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", "CaptchaCodeBackGround.jpg");
            var path = Common.CommonHelper.GetThumbnailImgPath(imgpath, 150, 50);
            var code = Common.CommonHelper.GetCaptchaCode(5);
            path +="<br/>"+ Common.CommonHelper.GetWatermarkImgPath(path, code);
            return Content(path);
            //return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
