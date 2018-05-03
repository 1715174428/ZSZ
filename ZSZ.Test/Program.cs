using System;
using System.IO;

namespace ZSZ.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var md5=Common.CommonHelper.GetMd5("123");
            Console.WriteLine(md5);
            var code = Common.CommonHelper.GetCaptchaCode(5);
            Console.WriteLine(code);
          var result=  Common.CommonHelper.SendEmail("1715174428@qq.com","zsz测试","邮件正文");
            Console.WriteLine(result);
            var imgpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", "CaptchaCodeBackGround.jpg");
            var path = Common.CommonHelper.GetThumbnailImgPath(imgpath, 50,50);
            Console.WriteLine(path);
            path = Common.CommonHelper.GetWatermarkImgPath(imgpath, code);
            Console.ReadKey();
        }
    }
}
