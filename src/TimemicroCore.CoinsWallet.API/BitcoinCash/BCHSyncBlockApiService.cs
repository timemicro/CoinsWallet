using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.BitcoinCash.Service;
using TimemicroCore.CoinsWallet.Sdk.BitcoinCash;

namespace TimemicroCore.CoinsWallet.Api.BitcoinCash
{
    public class BCHSyncBlockApiService : AbstractApiService<BCHSyncBlockReq, BCHSyncBlockResp>
    {
        private IWalletService WalletService { get; set; }

        public override string Name => "bch_syncblock";

        public BCHSyncBlockApiService(ApiServiceAppSettings appSettings, IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override BCHSyncBlockResp Execute(BCHSyncBlockReq req)
        {
            WalletService.SyncBlock();
            var resp = new BCHSyncBlockResp();
            resp.Signature = resp.SignByMD5(AppSettings.ApiKey);
            return resp;
        }
    }
}
