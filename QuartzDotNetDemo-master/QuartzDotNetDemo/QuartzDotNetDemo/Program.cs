using Autofac;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Specialized;
using Topshelf;
using Topshelf.Autofac;

namespace QuartzDotNetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(config =>
            {
                config.SetServiceName(JobService.ServiceName);
                config.SetDescription("Quartz.NET的demo");
                config.UseLog4Net();
                config.UseAutofacContainer(JobService.Container);

                config.Service<JobService>(setting =>
                {
                    JobService.InitSchedule(setting);
                    setting.ConstructUsingAutofacContainer();
                    setting.WhenStarted(o => o.Start());
                    setting.WhenStopped(o => o.Stop());
                });
            });
        }


        //static void Main(string[] args)
        //{
        //    //创建一个调度器工厂
        //    var props = new NameValueCollection
        //    {
        //        { "quartz.scheduler.instanceName", "QuartzDotNetDemo" }
        //    };
        //    var factory = new StdSchedulerFactory(props);

        //    //获取调度器
        //    var sched = factory.GetScheduler();
        //    sched.Start();

        //    //定义一个任务，关联"HelloJob"
        //    var job = JobBuilder.Create<HelloJob>()
        //        .WithIdentity("myJob", "group1")
        //        .Build();

        //    //由触发器每40秒触发执行一次任务
        //    var trigger = TriggerBuilder.Create()
        //        .WithIdentity("myTrigger", "group1")
        //        .StartNow()
        //        .WithSimpleSchedule(x => x
        //            .WithIntervalInSeconds(40)
        //            .RepeatForever())
        //        .Build();

        //    sched.ScheduleJob(job, trigger);
        //}
    }

    public class HelloJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("你好");
        }
    }
}
