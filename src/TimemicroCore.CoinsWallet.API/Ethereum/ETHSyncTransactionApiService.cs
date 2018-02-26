using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Ethereum.Service;
using TimemicroCore.CoinsWallet.Sdk.Ethereum;

namespace TimemicroCore.CoinsWallet.Api.Ethereum
{
    public class ETHSyncTransactionApiService : AbstractApiService<ETHSyncTransactionReq, ETHSyncTransactionResp>
    {
        private IWalletService WalletService { get; set; }

        public override string Name => "eth_synctransaction";

        public ETHSyncTransactionApiService(ApiServiceAppSettings appSettings, IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override ETHSyncTransactionResp Execute(ETHSyncTransactionReq req)
        {
            WalletService.SyncTransaction(1);
            var resp = new ETHSyncTransactionResp();
            resp.Signature = resp.SignByMD5(AppSettings.ApiKey);
            return resp;
        }
    }
}
