using Microsoft.Extensions.Configuration;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using TimemicroCore.CoinsWallet.Quartz.Bitcoin;
using TimemicroCore.CoinsWallet.Quartz.BitcoinCash;
using TimemicroCore.CoinsWallet.Quartz.LTC;

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
                AddBCHSendNotifyQuartzJob();
                AddBCHSyncBlockQuartzJob();
                AddBCHSyncTransactionQuartzJob();
            }

            if (Configuration.GetValue<bool>("CoinsWallet:Bitcoin:Enabled"))
            {
                AddBTCConfirmTransactionQuartzJob();
                AddBTCReceiveNotifyQuartzJob();
                AddBTCSendNotifyQuartzJob();
                AddBTCSyncBlockQuartzJob();
                AddBTCSyncTransactionQuartzJob();
            }

            if (Configuration.GetValue<bool>("CoinsWallet:Zcash:Enabled"))
            {
                AddZECConfirmTransactionQuartzJob();
                AddZECReceiveNotifyQuartzJob();
                AddZECSyncBlockQuartzJob();
                AddZECSyncTransactionQuartzJob();
            }

            if (Configuration.GetValue<bool>("CoinsWallet:Litecoin:Enabled"))
            {
                AddLTCConfirmTransactionQuartzJob();
                AddLTCReceiveNotifyQuartzJob();
                AddLTCSyncBlockQuartzJob();
                AddLTCSyncTransactionQuartzJob();
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
            IJobDetail job = JobBuilder.Create<BCHConfirmTransactionQuartzJob>()
                .WithIdentity("bchConfirmTransactionQuartzJob", "group1")
                .UsingJobData("ApiKey", Configuration["coinswallet:apikey"])
                .UsingJobData("ApiUrl", Configuration["coinswallet:apiurl"])
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("bchConfirmTransactionQuartzJobTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        async void AddBCHReceiveNotifyQuartzJob()
        {
            IJobDetail job = JobBuilder.Create<BCHReceiveNotifyQuartzJob>()
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

        async void AddBCHSendNotifyQuartzJob()
        {
            IJobDetail job = JobBuilder.Create<BCHSendNotifyQuartzJob>()
                .WithIdentity("bchSendNotifyQuartzJob", "group1")
                .UsingJobData("ApiKey", Configuration["coinswallet:apikey"])
                .UsingJobData("ApiUrl", Configuration["coinswallet:apiurl"])
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("bchSendNotifyQuartzJobTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        async void AddBCHSyncBlockQuartzJob()
        {
            IJobDetail job = JobBuilder.Create<BCHSyncBlockQuartzJob>()
                .WithIdentity("bchSyncBlockQuartzJob", "group1")
                .UsingJobData("ApiKey", Configuration["coinswallet:apikey"])
                .UsingJobData("ApiUrl", Configuration["coinswallet:apiurl"])
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("bchSyncBlockQuartzJobTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        async void AddBCHSyncTransactionQuartzJob()
        {
            IJobDetail job = JobBuilder.Create<BCHSyncTransactionQuartzJob>()
                .WithIdentity("bchSyncTransactionQuartzJob", "group1")
                .UsingJobData("ApiKey", Configuration["coinswallet:apikey"])
                .UsingJobData("ApiUrl", Configuration["coinswallet:apiurl"])
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("bchSyncTransactionQuartzJobTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        #endregion

        #region BTC

        async void AddBTCConfirmTransactionQuartzJob()
        {
            IJobDetail job = JobBuilder.Create<BTCConfirmTransactionQuartzJob>()
                .WithIdentity("btcConfirmTransactionQuartzJob", "group1")
                .UsingJobData("ApiKey", Configuration["coinswallet:apikey"])
                .UsingJobData("ApiUrl", Configuration["coinswallet:apiurl"])
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("btcConfirmTransactionQuartzJobTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        async void AddBTCReceiveNotifyQuartzJob()
        {
            IJobDetail job = JobBuilder.Create<BTCReceiveNotifyQuartzJob>()
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

        async void AddBTCSendNotifyQuartzJob()
        {
            IJobDetail job = JobBuilder.Create<BTCSendNotifyQuartzJob>()
                .WithIdentity("btcSendNotifyQuartzJob", "group1")
                .UsingJobData("ApiKey", Configuration["coinswallet:apikey"])
                .UsingJobData("ApiUrl", Configuration["coinswallet:apiurl"])
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("btcSendNotifyQuartzJobTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        async void AddBTCSyncBlockQuartzJob()
        {
            IJobDetail job = JobBuilder.Create<BTCSyncBlockQuartzJob>()
                .WithIdentity("btcSyncBlockQuartzJob", "group1")
                .UsingJobData("ApiKey", Configuration["coinswallet:apikey"])
                .UsingJobData("ApiUrl", Configuration["coinswallet:apiurl"])
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("btcSyncBlockQuartzJobTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        async void AddBTCSyncTransactionQuartzJob()
        {
            IJobDetail job = JobBuilder.Create<BTCSyncTransactionQuartzJob>()
                .WithIdentity("btcSyncTransactionQuartzJob", "group1")
                .UsingJobData("ApiKey", Configuration["coinswallet:apikey"])
                .UsingJobData("ApiUrl", Configuration["coinswallet:apiurl"])
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("btcSyncTransactionQuartzJobTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        #endregion

        #region ZEC

        async void AddZECConfirmTransactionQuartzJob()
        {
            IJobDetail job = JobBuilder.Create<Jobs.ZECConfirmTransactionQuartzJob>()
                .WithIdentity("zecConfirmTransactionQuartzJob", "group1")
                .UsingJobData("ApiKey", Configuration["coinswallet:apikey"])
                .UsingJobData("ApiUrl", Configuration["coinswallet:apiurl"])
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("zecConfirmTransactionQuartzJobTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        async void AddZECReceiveNotifyQuartzJob()
        {
            IJobDetail job = JobBuilder.Create<Jobs.ZECReceiveNotifyQuartzJob>()
                .WithIdentity("zecReceiveNotifyQuartzJob", "group1")
                .UsingJobData("ApiKey", Configuration["coinswallet:apikey"])
                .UsingJobData("ApiUrl", Configuration["coinswallet:apiurl"])
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("zecReceiveNotifyQuartzJobTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        async void AddZECSyncBlockQuartzJob()
        {
            IJobDetail job = JobBuilder.Create<Jobs.ZECSyncBlockQuartzJob>()
                .WithIdentity("zecSyncBlockQuartzJob", "group1")
                .UsingJobData("ApiKey", Configuration["coinswallet:apikey"])
                .UsingJobData("ApiUrl", Configuration["coinswallet:apiurl"])
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("zecSyncBlockQuartzJobTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        async void AddZECSyncTransactionQuartzJob()
        {
            IJobDetail job = JobBuilder.Create<Jobs.ZECSyncTransactionQuartzJob>()
                .WithIdentity("zecSyncTransactionQuartzJob", "group1")
                .UsingJobData("ApiKey", Configuration["coinswallet:apikey"])
                .UsingJobData("ApiUrl", Configuration["coinswallet:apiurl"])
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("zecSyncTransactionQuartzJobTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        #endregion

        #region LTC

        async void AddLTCConfirmTransactionQuartzJob()
        {
            IJobDetail job = JobBuilder.Create<LTCConfirmTransactionQuartzJob>()
                .WithIdentity("ltcConfirmTransactionQuartzJob", "group1")
                .UsingJobData("ApiKey", Configuration["coinswallet:apikey"])
                .UsingJobData("ApiUrl", Configuration["coinswallet:apiurl"])
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("ltcConfirmTransactionQuartzJobTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        async void AddLTCReceiveNotifyQuartzJob()
        {
            IJobDetail job = JobBuilder.Create<LTCReceiveNotifyQuartzJob>()
                .WithIdentity("ltcReceiveNotifyQuartzJob", "group1")
                .UsingJobData("ApiKey", Configuration["coinswallet:apikey"])
                .UsingJobData("ApiUrl", Configuration["coinswallet:apiurl"])
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("ltcReceiveNotifyQuartzJobTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        async void AddLTCSyncBlockQuartzJob()
        {
            IJobDetail job = JobBuilder.Create<LTCSyncBlockQuartzJob>()
                .WithIdentity("ltcSyncBlockQuartzJob", "group1")
                .UsingJobData("ApiKey", Configuration["coinswallet:apikey"])
                .UsingJobData("ApiUrl", Configuration["coinswallet:apiurl"])
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("ltcSyncBlockQuartzJobTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        async void AddLTCSyncTransactionQuartzJob()
        {
            IJobDetail job = JobBuilder.Create<LTCSyncTransactionQuartzJob>()
                .WithIdentity("ltcSyncTransactionQuartzJob", "group1")
                .UsingJobData("ApiKey", Configuration["coinswallet:apikey"])
                .UsingJobData("ApiUrl", Configuration["coinswallet:apiurl"])
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("ltcSyncTransactionQuartzJobTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        #endregion
    }
}
