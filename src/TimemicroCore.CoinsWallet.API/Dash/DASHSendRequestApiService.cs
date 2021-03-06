﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimemicroCore.CoinsWallet.Dash.PO;
using TimemicroCore.CoinsWallet.Sdk.Dash;

namespace TimemicroCore.CoinsWallet.Api.Dash
{
    public class DASHSendRequestApiService : AbstractApiService<DASHSendRequestReq, DASHSendRequestResp>
    {
        private CoinsWalletDbContext context;

        public override string Name => "dash_sendrequest";

        public DASHSendRequestApiService(ApiServiceAppSettings appSettings, CoinsWalletDbContext context)
        {
            AppSettings = appSettings;
            this.context = context;
        }

        public override DASHSendRequestResp Execute(DASHSendRequestReq req)
        {
            var resp = new DASHSendRequestResp();

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
