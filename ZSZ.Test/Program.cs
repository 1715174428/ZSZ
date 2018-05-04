using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.IO;
using NLog.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using ZSZ.MVC;
using Quartz.Impl;
using Quartz;
using Quartz.Spi;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace ZSZ.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //var md5 = Common.CommonHelper.GetMd5("123");
            //Console.WriteLine(md5);
            //var code = Common.CommonHelper.GetCaptchaCode(5);
            //Console.WriteLine(code);
            //var result = Common.CommonHelper.SendEmail("1715174428@qq.com", "zsz测试", "邮件正文");
            //Console.WriteLine(result);
            //var imgpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", "CaptchaCodeBackGround.jpg");
            //var path = Common.CommonHelper.GetThumbnailImgPath(imgpath, 50, 50);
            //Console.WriteLine(path);
            //path = Common.CommonHelper.GetWatermarkImgPath(imgpath, code);

            //var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            //try
            //{
            //    logger.Debug("调试日志");
            //    BuildWebHost(args).Run();
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex, "错误日志");
            //}
            //finally {
            //    NLog.LogManager.Shutdown();
            //}
            Console.WriteLine("设定任务任务");
            Task.Run(async ()=> {
                try
                {
                    //从工厂中获取调度程序实例
                    NameValueCollection props = new NameValueCollection() { { "quartz.serializer.type", "binary" } };
                    StdSchedulerFactory factory = new StdSchedulerFactory(props);
                    IScheduler scheduler = await factory.GetScheduler();
                    //开启调度器
                    await scheduler.Start();
                    //定义这个工作,并将其绑定到我们的IJob实现类
                    IJobDetail job = JobBuilder.Create<MyJob>().WithIdentity("job1", "group1").Build();
                    //触发作业立即运行,然后每十秒重复一次,无限循环
                    ITrigger trigger = TriggerBuilder.Create().WithIdentity("trigger1", "group1").StartNow().WithSimpleSchedule(x => x.WithIntervalInSeconds(10).RepeatForever()).Build();
                    //告诉Quartz使用我们的触发器来安排作业
                    await scheduler.ScheduleJob(job, trigger);
                    //等待60秒
                    await Task.Delay(TimeSpan.FromSeconds(60));
                    //关闭调度程序
                    await scheduler.Shutdown();

                }
                catch (Exception ex)
                {

                    await Console.Error.WriteLineAsync(ex.ToString());
                }
            });
           
            Console.WriteLine("任务设定完毕等待执行");
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
