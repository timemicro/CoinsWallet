using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimemicroCore.CoinsWallet.Litecoin.PO;
using TimemicroCore.CoinsWallet.Sdk.Litecoin;

namespace TimemicroCore.CoinsWallet.Api.Litecoin
{
    public class LTCSendRequestApiService : AbstractApiService<LTCSendRequestReq, LTCSendRequestResp>
    {
        private CoinsWalletDbContext context;

        public override string Name => "ltc_sendrequest";

        public LTCSendRequestApiService(ApiServiceAppSettings appSettings, CoinsWalletDbContext context)
        {
            AppSettings = appSettings;
            this.context = context;
        }

        public override LTCSendRequestResp Execute(LTCSendRequestReq req)
        {
            var resp = new LTCSendRequestResp();

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
