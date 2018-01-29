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
        public Startup()
        { }

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

        async void AddBTCSyncBlockQuartzJob()
        {
            IJobDetail btcSyncBlockQuartzJob = JobBuilder.Create<Jobs.BTCSyncBlockQuartzJob>()
                .WithIdentity("btcSyncBlockQuartzJob", "group1")
                .Build();

            ITrigger btcSyncBlockQuartzJobTrigger = TriggerBuilder.Create()
                .WithIdentity("btcSyncBlockQuartzJobTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(1)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(btcSyncBlockQuartzJob, btcSyncBlockQuartzJobTrigger);
        }

        async void AddBTCSyncTransactionQuartzJob()
        {
            IJobDetail btcSyncTransactionQuartzJob = JobBuilder.Create<Jobs.BTCSyncTransactionQuartzJob>()
                .WithIdentity("btcSyncTransactionQuartzJob", "group1")
                .Build();

            ITrigger btcSyncTransactionQuartzJobTrigger = TriggerBuilder.Create()
                .WithIdentity("btcSyncTransactionQuartzJob", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(30)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(btcSyncTransactionQuartzJob, btcSyncTransactionQuartzJobTrigger);

        }
    }
}
