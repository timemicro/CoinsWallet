using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Ethereum.PO
{
    public class TransactionDetailsPO
    {
        public long Id { get; set; }

        public string TxId { get; set; }

        public string Address { get; set; }

        public decimal Amount { get; set; }

        public string Category { get; set; }
    }
}
