using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimemicroCore.CoinsWallet.Bitcoin.PO;
using TimemicroCore.CoinsWallet.Sdk.Bitcoin;

namespace TimemicroCore.CoinsWallet.Api.Impl
{
    public class BTCSendRequestApiService : AbstractApiService<BTCSendRequestReq, BTCSendRequestResp>
    {
        private CoinsWalletDbContext context;

        public override string Name => "btc_sendrequest";

        public BTCSendRequestApiService(ApiServiceAppSettings appSettings, CoinsWalletDbContext context)
        {
            AppSettings = appSettings;
            this.context = context;
        }

        public override BTCSendRequestResp Execute(BTCSendRequestReq req)
        {
            var resp = new BTCSendRequestResp();

            var sendRequest = context.SendRequests.Where(x => x.OutRequestNo == req.OutRequestNo).FirstOrDefault();
            if (sendRequest != null)
            {
                resp.RespCode = "10004";
                resp.RespMessage = "申请单号重复";
            }
            else
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.SendRequests.Add(new SendRequestPO
                        {
                            Address = req.Address,
                            Amount = req.Amount,
                            OutRequestNo = req.OutRequestNo,
                            State = 0
                        });
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception)
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
            resp.Signature = resp.SignByMD5(AppSettings.ApiKey);
            return resp;
        }
    }
}
