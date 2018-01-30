using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.BitcoinCash.PO
{
    public class SendNotifyLogPO
    {
        public string TxId { get; set; }

        public string Address { get; set; }

        public int NotifiedCount { get; set; }

        public string NotifyResponseText { get; set; }

        public DateTime NextNotifyTime { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
