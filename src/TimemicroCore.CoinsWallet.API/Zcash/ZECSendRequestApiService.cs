using System;
using System.Linq;
using TimemicroCore.CoinsWallet.Zcash.PO;
using TimemicroCore.CoinsWallet.Sdk.Zcash;

namespace TimemicroCore.CoinsWallet.Api.Zcash
{
    public class ZECSendRequestApiService : AbstractApiService<ZECSendRequestReq, ZECSendRequestResp>
    {
        private CoinsWalletDbContext context;

        public override string Name => "zec_sendrequest";

        public ZECSendRequestApiService(ApiServiceAppSettings appSettings, CoinsWalletDbContext context)
        {
            AppSettings = appSettings;
            this.context = context;
        }

        public override ZECSendRequestResp Execute(ZECSendRequestReq req)
        {
            var resp = new ZECSendRequestResp();

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
