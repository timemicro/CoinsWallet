using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Ethereum.Service;
using TimemicroCore.CoinsWallet.Sdk.Ethereum;

namespace TimemicroCore.CoinsWallet.Api.Ethereum
{
    public class ETHSyncBlockApiService : AbstractApiService<ETHSyncBlockReq, ETHSyncBlockResp>, IDisposable
    {
        private IWalletService WalletService { get; set; }

        public override string Name => "eth_syncblock";

        public ETHSyncBlockApiService(ApiServiceAppSettings appSettings, IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override ETHSyncBlockResp Execute(ETHSyncBlockReq req)
        {
            WalletService.SyncBlock();
            var resp = new ETHSyncBlockResp();
            resp.Signature = resp.SignByMD5(AppSettings.ApiKey);
            return resp;
        }

        public void Dispose()
        {
            AppSettings = null;
            WalletService = null;
        }
    }
}
