using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.IO;
using NLog.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using ZSZ.MVC;

namespace ZSZ.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var md5 = Common.CommonHelper.GetMd5("123");
            Console.WriteLine(md5);
            var code = Common.CommonHelper.GetCaptchaCode(5);
            Console.WriteLine(code);
            var result = Common.CommonHelper.SendEmail("1715174428@qq.com", "zsz测试", "邮件正文");
            Console.WriteLine(result);
            var imgpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", "CaptchaCodeBackGround.jpg");
            var path = Common.CommonHelper.GetThumbnailImgPath(imgpath, 50, 50);
            Console.WriteLine(path);
            path = Common.CommonHelper.GetWatermarkImgPath(imgpath, code);

            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("调试日志");
                BuildWebHost(args).Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "错误日志");
            }
            finally {
                NLog.LogManager.Shutdown();
            }
           

            Console.ReadKey();
        }
        public static IWebHost BuildWebHost(string[] args) =>
         WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
        })
        .UseNLog()  // NLog: setup NLog for Dependency injection
        .Build();
    }
}
