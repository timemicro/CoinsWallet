using Microsoft.Extensions.Configuration;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace TimemicroCore.CoinsWallet.Quartz
{
    public class Startup
    {
        public IConfiguration Configuration  { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IScheduler scheduler;

        public async void Start()
        {
            NameValueCollection props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            scheduler = await factory.GetScheduler();

            await scheduler.Start();

            AddBTCConfirmTransactionQuartzJob();
            AddBTCReceiveNotifyQuartzJob();
            AddBTCSyncBlockQuartzJob();
            AddBTCSyncTransactionQuartzJob();
        }

        public async void Stop()
        {
            if (scheduler != null)
            {
                await scheduler.Shutdown();
            }
        }

        async void AddBTCConfirmTransactionQuartzJob()
        {
            IJobDetail job = JobBuilder.Create<Jobs.BTCConfirmTransactionQuartzJob>()
                .WithIdentity("btcConfirmTransactionQuartzJob", "group1")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("btcConfirmTransactionQuartzJobTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(30)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        async void AddBTCReceiveNotifyQuartzJob()
        {
            IJobDetail job = JobBuilder.Create<Jobs.BTCReceiveNotifyQuartzJob>()
                .WithIdentity("btcReceiveNotifyQuartzJob", "group1")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("btcReceiveNotifyQuartzJobTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        async void AddBTCSyncBlockQuartzJob()
        {
            IJobDetail job = JobBuilder.Create<Jobs.BTCSyncBlockQuartzJob>()
                .WithIdentity("btcSyncBlockQuartzJob", "group1")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("btcSyncBlockQuartzJobTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(1)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        async void AddBTCSyncTransactionQuartzJob()
        {
            IJobDetail job = JobBuilder.Create<Jobs.BTCSyncTransactionQuartzJob>()
                .WithIdentity("btcSyncTransactionQuartzJob", "group1")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("btcSyncTransactionQuartzJobTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(30)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        
    }
}
