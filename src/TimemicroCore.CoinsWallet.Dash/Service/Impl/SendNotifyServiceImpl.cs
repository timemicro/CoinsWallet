using log4net;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using TimemicroCore.CoinsWallet.Dash.PO;
using TimemicroCore.CoinsWallet.Network;
using TimemicroCore.CoinsWallet.Sdk.Dash;

namespace TimemicroCore.CoinsWallet.Dash.Service.Impl
{
    public class SendNotifyServiceImpl : ISendNotifyService
    {
        static ILog logger = LogManager.GetLogger("NETCoreRepository", typeof(SendNotifyServiceImpl));

        private IConfiguration configuration;

        private CoinsWalletDbContext context;

        public SendNotifyServiceImpl(IConfiguration configuration, CoinsWalletDbContext context)
        {
            this.configuration = configuration;
            this.context = context;
        }

        private DateTime CalculateNextNotifyTime(DateTime dt, int count)
        {
            if (count == 1)
            {
                return dt.AddMinutes(1);
            }
            else if (count == 2)
            {
                return dt.AddMinutes(5);
            }
            else if (count == 3)
            {
                return dt.AddMinutes(15);
            }
            else if (count == 4)
            {
                return dt.AddMinutes(30);
            }
            else if (count == 5)
            {
                return dt.AddMinutes(60);
            }
            else if (count == 6)
            {
                return dt.AddMinutes(120);
            }
            else
            {
                return dt;
            }
        }

        public void Notify()
        {
            var logs = context.SendNotifyLogs
                .Where(x => x.NextNotifyTime > DateTime.Now.Date.AddDays(-1)
                    && x.NextNotifyTime < DateTime.Now
                    && x.NotifiedCount <= 6)
                .OrderBy(x => x.NotifiedCount).Take(5);

            if (logs == null || logs.Count() == 0)
            {
                return;
            }

            var result = new DASHSendNotifyResult()
            {
                Data = new List<DASHSendNotifyResultDataItem>()
            };

            foreach (var item in logs)
            {
                result.Data.Add(new DASHSendNotifyResultDataItem()
                {
                    OutRequestNo = item.OutRequestNo,
                    Address = item.Address,
                    TxId = item.TxId
                });
            }

            var responseText = string.Empty;
            var http = WebRequest.CreateHttp(configuration["CoinsWallet:Dash:SendNotifyUrl"]);
            try
            {
                responseText = http.PostJson(result.ToJson());
            }
            catch (Exception ex)
            {
                responseText = ex.Message;

                logger.Error(ex);
            }

            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in logs)
                    {
                        item.NotifiedCount += 1;
                        item.NotifyResponseText = responseText;

                        if (!string.Equals(responseText, "success", StringComparison.CurrentCultureIgnoreCase))
                        {
                            item.NextNotifyTime = CalculateNextNotifyTime(item.NextNotifyTime, item.NotifiedCount);
                        }
                    }

                    context.SaveChanges();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();

                    logger.Error(ex);
                }
                finally
                {
                    tran.Dispose();
                }
            }
        }
    }
}
