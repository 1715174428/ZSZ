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
            Console.Out.WriteLine("执行任务中" + DateTime.Now.ToString());
            Console.Out.WriteLine("任务执行完毕" + DateTime.Now.ToString());
            return Task.CompletedTask;
        }
    }
}
