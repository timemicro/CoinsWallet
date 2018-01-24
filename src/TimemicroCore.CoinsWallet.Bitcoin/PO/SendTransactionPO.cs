using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Bitcoin.PO
{
    public class SendTransactionPO
    {
        public string TxId { get; set; }

        public decimal Amount { get; set; }

        public decimal Fee { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
