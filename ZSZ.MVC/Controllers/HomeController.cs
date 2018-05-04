using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog.Web;
using ZSZ.MVC.Models;

namespace ZSZ.MVC.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ILogger<BaseController> logger) : base(logger)
        {
        }

        public IActionResult Nlog()
        {
            _logger.LogDebug("这是记录调试日志");
            _logger.LogWarning("这是记录警告日志");
            _logger.LogError("这是记录错误日志");
            return Content("记录日志成功");
        }
        public IActionResult Index()
        {
            var imgpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", "CaptchaCodeBackGround.jpg");
            var path = Common.CommonHelper.GetThumbnailImgPath(imgpath, 150, 50);
            var code = Common.CommonHelper.GetCaptchaCode(5);
            path += "<br/>" + Common.CommonHelper.GetWatermarkImgPath(path, code);
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
