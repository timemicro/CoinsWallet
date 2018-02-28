using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Ethereum.Service;
using TimemicroCore.CoinsWallet.Sdk.Ethereum;

namespace TimemicroCore.CoinsWallet.Api.Ethereum
{
    public class ETHConfirmTransactionApiService : AbstractApiService<ETHConfirmTransactionReq, ETHConfirmTransactionResp>
    {
        private IWalletService WalletService { get; }

        public override string Name => "eth_confirmtransaction";

        public ETHConfirmTransactionApiService(ApiServiceAppSettings appSettings, IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override ETHConfirmTransactionResp Execute(ETHConfirmTransactionReq req)
        {
            WalletService.ConfirmTransaction();

            var resp = new ETHConfirmTransactionResp();
            resp.Signature = resp.SignByMD5(AppSettings.ApiKey);
            return resp;
        }
    }
}
