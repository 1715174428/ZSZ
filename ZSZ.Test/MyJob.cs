using NLog.Web;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.Test
{
    public class MyJob : IJob
    {
   
        Task IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                Console.Out.WriteLine("执行任务中" + DateTime.Now.ToString());
                Console.Out.WriteLine("任务执行完毕" + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {

                var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
                logger.Error(ex,"执行定时任务出现异常!");
            }
            return Task.CompletedTask;
        }
    }
}
