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

            if (Configuration.GetValue<bool>("CoinsWallet:BitcoinCash:Enabled"))
            {
                AddBCHConfirmTransactionQuartzJob();
                AddBCHReceiveNotifyQuartzJob();
                AddBCHSyncBlockQuartzJob();
                AddBCHSyncTransactionQuartzJob();
            }

            if (Configuration.GetValue<bool>("CoinsWallet:Bitcoin:Enabled"))
            {
                AddBTCConfirmTransactionQuartzJob();
                AddBTCReceiveNotifyQuartzJob();
                AddBTCSyncBlockQuartzJob();
                AddBTCSyncTransactionQuartzJob();
            }
        }

        public async void Stop()
        {
            if (scheduler != null)
            {
                await scheduler.Shutdown();
            }
        }

        #region BCH

        async void AddBCHConfirmTransactionQuartzJob()
        {
            IJobDetail job = JobBuilder.Create<Jobs.BCHConfirmTransactionQuartzJob>()
                .WithIdentity("bchConfirmTransactionQuartzJob", "group1")
                .UsingJobData("ApiKey", Configuration["coinswallet:apikey"])
                .UsingJobData("ApiUrl", Configuration["coinswallet:apiurl"])
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("bchConfirmTransactionQuartzJobTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(30)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        async void AddBCHReceiveNotifyQuartzJob()
        {
            IJobDetail job = JobBuilder.Create<Jobs.BCHReceiveNotifyQuartzJob>()
                .WithIdentity("bchReceiveNotifyQuartzJob", "group1")
                .UsingJobData("ApiKey", Configuration["coinswallet:apikey"])
                .UsingJobData("ApiUrl", Configuration["coinswallet:apiurl"])
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("bchReceiveNotifyQuartzJobTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        async void AddBCHSyncBlockQuartzJob()
        {
            IJobDetail job = JobBuilder.Create<Jobs.BCHSyncBlockQuartzJob>()
                .WithIdentity("bchSyncBlockQuartzJob", "group1")
                .UsingJobData("ApiKey", Configuration["coinswallet:apikey"])
                .UsingJobData("ApiUrl", Configuration["coinswallet:apiurl"])
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("bchSyncBlockQuartzJobTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(1)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        async void AddBCHSyncTransactionQuartzJob()
        {
            IJobDetail job = JobBuilder.Create<Jobs.BCHSyncTransactionQuartzJob>()
                .WithIdentity("bchSyncTransactionQuartzJob", "group1")
                .UsingJobData("ApiKey", Configuration["coinswallet:apikey"])
                .UsingJobData("ApiUrl", Configuration["coinswallet:apiurl"])
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("bchSyncTransactionQuartzJobTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(30)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        #endregion

        #region BTC

        async void AddBTCConfirmTransactionQuartzJob()
        {
            IJobDetail job = JobBuilder.Create<Jobs.BTCConfirmTransactionQuartzJob>()
                .WithIdentity("btcConfirmTransactionQuartzJob", "group1")
                .UsingJobData("ApiKey", Configuration["coinswallet:apikey"])
                .UsingJobData("ApiUrl", Configuration["coinswallet:apiurl"])
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
                .UsingJobData("ApiKey", Configuration["coinswallet:apikey"])
                .UsingJobData("ApiUrl", Configuration["coinswallet:apiurl"])
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
                .UsingJobData("ApiKey", Configuration["coinswallet:apikey"])
                .UsingJobData("ApiUrl", Configuration["coinswallet:apiurl"])
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
                .UsingJobData("ApiKey", Configuration["coinswallet:apikey"])
                .UsingJobData("ApiUrl", Configuration["coinswallet:apiurl"])
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

        #endregion
    }
}
