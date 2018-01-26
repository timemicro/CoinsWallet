using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimemicroCore.CoinsWallet.Bitcoin.Service;

namespace TimemicroCore.CoinsWallet.Bitcoin.Jobs
{
    public class SyncBlockQuartzJob : IJob
    {
        private IWalletService WalletService { get; set; }

        public Task Execute(IJobExecutionContext context)
        {
            WalletService.SyncBlock();

            return null;
        }
    }
}
