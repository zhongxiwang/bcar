
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace bcar.service
{

    /// <summary>
    /// 任务委托
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public delegate Task JobAction(IJobExecutionContext context);

    /// <summary>
    /// 任务管理服务器
    /// </summary>
    public class TaskManagerService
    {
        const string defaultScheduler = "taskM";
        Dictionary<string, IScheduler> SchedulerList = new Dictionary<string, IScheduler>();
        readonly StdSchedulerFactory factory;
        private TaskManagerService()
        {
            NameValueCollection props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
            this.factory = new StdSchedulerFactory(props);
            Task.Run(async () =>
            {
                var sched = await factory.GetScheduler();
                SchedulerList[defaultScheduler] = sched;
                await sched.Start();
            }).Wait();
            
        }

        readonly static TaskManagerService _tms = new TaskManagerService();

        /// <summary>
        /// 创建任务调度器
        /// </summary>
        /// <returns></returns>
        public static TaskManagerService Factory()
        {
            return _tms;
        }

        /// <summary>
        /// 创建Scheduler
        /// </summary>
        /// <param name="name">Scheduler名称</param>
        /// <returns></returns>
        public async Task CareteScheduler(string name = defaultScheduler)
        {
            if (this.SchedulerList.ContainsKey(name)) return;
            this.SchedulerList[name] = await factory.GetScheduler();
            await this.SchedulerList[name].Start();
        }
        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="jobi"></param>
        /// <param name="groupName">监听的任务所属组</param>
        /// <param name="name">Scheduler名称</param>
        /// <returns></returns>
        public async Task AddJobListener(IJobListener jobi, string groupName, string name = defaultScheduler)
        {
            this.SchedulerList[name].ListenerManager.AddJobListener(jobi, GroupMatcher<JobKey>.GroupEquals(groupName));
        }
        /// <summary>
        /// 创建任务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public async Task<IJobDetail> CreateJob<T>(string jobName,string groupName, IDictionary<string, object>  datamap=null) where T:IJob
        {
            var tmp = JobBuilder.Create<T>().WithIdentity(jobName, groupName);
            if( datamap!=null) tmp.SetJobData(new JobDataMap( datamap));
            return tmp.Build();
        }

        /// <summary>
        /// 创建触发器
        /// </summary>
        /// <param name="triggerName">触发器名字</param>
        /// <param name="groupName">触发器所属组</param>
        /// <param name="time">开始执行时间</param>
        /// <returns></returns>
        public async Task<ITrigger> CreateTrigger(string triggerName, string groupName,DateTime time, IJobDetail job)
        {
          return TriggerBuilder.Create()
                .WithIdentity(triggerName, groupName)
                .StartAt(time)
                .ForJob(job)
                .Build();
        }
        public async Task<ITrigger> CreateTrigger(string triggerName, string groupName, DateTime time, string jobName,string jobGroup)
        {
            return TriggerBuilder.Create()
                  .WithIdentity(triggerName, groupName)
                  .StartAt(time)
                  .ForJob(jobName,jobGroup)
                  .Build();
        }
        /// <summary>
        /// 添加触发器事件
        /// </summary>
        /// <param name="jobi">事件</param>
        /// <param name="groupName">监听的事件组</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task AddTriggerListener(ITriggerListener jobi, string groupName, string name = defaultScheduler)
        {
            this.SchedulerList[name].ListenerManager.AddTriggerListener(jobi, GroupMatcher<TriggerKey>.GroupEquals(groupName));
        }
        public void deleteJob(string jobname, string name = defaultScheduler)
        {
            string group = "_group";
            var jobnames = "job_" + jobname;
            SchedulerList[name].DeleteJob(new JobKey(jobnames, jobnames+group));
        }
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="job"></param>
        /// <param name="trigger"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task Run(IJobDetail job,ITrigger trigger,string name=defaultScheduler)
        {
            await SchedulerList[name].ScheduleJob(job, trigger);
        }
        /// <summary>
        /// 自动执行方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public async Task AutoRun<T>(string name, DateTime dt, IDictionary<string, object>  datamap=null) where T:IJob
        {
            string group = "_group";
            var jobname= "job_" + name;
            var triggername = "trigger_" + name;
            Task<IJobDetail> task1 = Task.Run<IJobDetail>(() =>CreateJob<T>(jobname, jobname + group, datamap));
            Task<ITrigger> task2 = Task.Run<ITrigger>(() => CreateTrigger(triggername, triggername + group, dt,jobname,jobname+group));
            Task.WaitAll(task1, task2);
            await Run(task1.Result, task2.Result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public async Task AutoRun(DateTime dt, JobAction action, string name = "" ) 
        {
            if(string.IsNullOrWhiteSpace(name))name = Guid.NewGuid().ToString();
            string group = "_group";
            var jobname = "job_" + name;
            var triggername = "trigger_" + name;
            Task<IJobDetail> task1 = Task.Run<IJobDetail>(() => CreateJob<DynamicTask>(jobname, jobname + group,new Dictionary<string, object>
            {
                {"jobaction",action }
            }));
            Task<ITrigger> task2 = Task.Run<ITrigger>(() => CreateTrigger(triggername, triggername + group, dt, jobname, jobname + group));
            Task.WaitAll(task1, task2);
            await Run(task1.Result, task2.Result);
        }
        /// <summary>
        /// 关闭调度器
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task shutdown(string name = defaultScheduler)
        {
            return  Task.Run(() =>
            {
                SchedulerList[name].Shutdown();
            });
        }
    }
}
