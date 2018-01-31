using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Bitcoin.Service;
using TimemicroCore.CoinsWallet.Sdk.Bitcoin;

namespace TimemicroCore.CoinsWallet.Api.Bitcoin
{
    public class BTCSyncTransactionApiService : AbstractApiService<BTCSyncTransactionReq, BTCSyncTransactionResp>
    {
        private IWalletService WalletService { get; set; }

        public override string Name => "btc_synctransaction";

        public BTCSyncTransactionApiService(ApiServiceAppSettings appSettings, IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override BTCSyncTransactionResp Execute(BTCSyncTransactionReq req)
        {
            WalletService.SyncTransaction(1);
            var resp = new BTCSyncTransactionResp();
            resp.Signature = resp.SignByMD5(AppSettings.ApiKey);
            return resp;
        }
    }
}
