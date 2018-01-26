using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Bitcoin.Service;
using TimemicroCore.CoinsWallet.Sdk.Bitcoin;

namespace TimemicroCore.CoinsWallet.Api.Impl
{
    public class BTCConfirmTransactionApiService : AbstractApiService<BTCConfirmTransactionReq, BTCConfirmTransactionResp>
    {
        private IWalletService WalletService { get; set; }

        public override string Name => "btc_confirmtransaction";

        public BTCConfirmTransactionApiService(IWalletService walletService)
        {
            WalletService = walletService;
        }

        public override BTCConfirmTransactionResp Execute(BTCConfirmTransactionReq req)
        {
            WalletService.ConfirmTransaction();

            return new BTCConfirmTransactionResp();
        }
    }
}
