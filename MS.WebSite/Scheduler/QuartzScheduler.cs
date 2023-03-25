using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using MS.BusinessLayer.IServices;
using MS.BusinessLayer.Services;
using MS.DataLayer.Concrete;
using MS.DataLayer.Entities;

namespace MS.WebSite.Scheduler
{
    public class QuartzScheduler
    {
        public void Run()
        {
            try
            {
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
                scheduler.Start();

                //subscriptions
                IJobDetail subscriptionsJob = JobBuilder.Create<SubscriptionChangeStatusJob>()
                    .WithIdentity("ChangeStatus", "Subscriptions")
                    .Build();

                ITrigger subscriptionsTrigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1sc", "Subscriptions")
                    //.WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(00, 1))
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInHours(24)
                        .RepeatForever())
                    .Build();

                scheduler.ScheduleJob(subscriptionsJob, subscriptionsTrigger);


                //trainings
                IJobDetail trainingsJob = JobBuilder.Create<TrainingChangeStatusJob>()
                    .WithIdentity("job1tr", "Trainings")
                    .Build();

                ITrigger trainingTrigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1tr", "Trainings")
                    //.WithCronSchedule("10 0 / 2 * ** ?")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(60)
                        .RepeatForever())
                    .Build();

                scheduler.ScheduleJob(trainingsJob, trainingTrigger);


                //frozen subscriptions
                IJobDetail frozenSubscriptionsJob = JobBuilder.Create<ChangeStatusOfFreezeSubscriptions>()
                    .WithIdentity("ChangeFrozenStatus", "Subscriptions2")
                    .Build();

                ITrigger frozenSubscriptionsTrigger = TriggerBuilder.Create()
                    .WithIdentity("trigger2sc", "Subscriptions")
                    //.WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(00, 1))
                    .StartNow()
                    .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(60)
                        //.WithIntervalInHours(24)
                        .RepeatForever())
                    .Build();

                scheduler.ScheduleJob(frozenSubscriptionsJob, frozenSubscriptionsTrigger);

            }
            catch(Exception e){}
        }
    }



    [DisallowConcurrentExecution]
    public class SubscriptionChangeStatusJob : IJob
    {
        private readonly SubscriptionService _subscriptionService = new SubscriptionService(new SubscriptionRepository(new ManagmentSystemContext()));
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                _subscriptionService.ChangeSubscriptionStatus();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    [DisallowConcurrentExecution]
    public class TrainingChangeStatusJob : IJob
    {
        private readonly TrainingService _trainingService = new TrainingService(new TrainingRepository(new ManagmentSystemContext()));
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                _trainingService.ChangeTrainingStatus();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    [DisallowConcurrentExecution]
    public class ChangeStatusOfFreezeSubscriptions : IJob
    {
        private readonly SubscriptionService _subscriptionService = new SubscriptionService(new SubscriptionRepository(new ManagmentSystemContext()));
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                _subscriptionService.ChangeStatusIfFrozenDayPassed();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
