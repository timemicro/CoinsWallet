using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Bitcoin.Service;
using TimemicroCore.CoinsWallet.Sdk.Bitcoin;

namespace TimemicroCore.CoinsWallet.Api.Impl
{
    public class BTCSyncBlockApiService : AbstractApiService<BTCSyncBlockReq, BTCSyncBlockResp>
    {
        private IWalletService WalletService { get; set; }

        public override string Name => "btc_syncblock";

        public BTCSyncBlockApiService(IWalletService walletService)
        {
            WalletService = walletService;
        }

        public override BTCSyncBlockResp Execute(BTCSyncBlockReq req)
        {
            WalletService.SyncBlock();
            return new BTCSyncBlockResp();
        }
    }
}
