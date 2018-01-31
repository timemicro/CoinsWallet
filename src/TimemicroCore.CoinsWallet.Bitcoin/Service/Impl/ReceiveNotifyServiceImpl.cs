using log4net;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using TimemicroCore.CoinsWallet.Bitcoin.PO;
using TimemicroCore.CoinsWallet.Network;
using TimemicroCore.CoinsWallet.Sdk.Bitcoin;

namespace TimemicroCore.CoinsWallet.Bitcoin.Service.Impl
{
    public class ReceiveNotifyServiceImpl : IReceiveNotifyService
    {
        static ILog logger = LogManager.GetLogger("NETCoreRepository", typeof(ReceiveNotifyServiceImpl));

        private IConfiguration configuration;

        private CoinsWalletDbContext context;

        public ReceiveNotifyServiceImpl(IConfiguration configuration, CoinsWalletDbContext context)
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
            var logs = context.ReceiveNotifyLogs
                .Where(x => x.NextNotifyTime > DateTime.Now.Date.AddDays(-1)
                    && x.NextNotifyTime < DateTime.Now
                    && x.NotifiedCount <= 6)
                .OrderBy(x => x.NotifiedCount).Take(5);

            if (logs == null || logs.Count() == 0)
            {
                return;
            }

            var result = new BTCReceiveNotifyResult()
            {
                Data = new List<BTCReceiveNotifyResultDataItem>()
            };

            foreach (var item in logs)
            {
                result.Data.Add(new BTCReceiveNotifyResultDataItem()
                {
                    Address = item.Address,
                    Amount = item.Amount,
                    TxId = item.TxId
                });
            }

            var responseText = string.Empty;
            var http = WebRequest.CreateHttp(configuration["CoinsWallet:Bitcoin:ReceiveNotifyUrl"]);
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
