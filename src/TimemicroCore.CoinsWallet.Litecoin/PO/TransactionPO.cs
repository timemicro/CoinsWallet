using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Litecoin.PO
{
    public class TransactionPO
    {
        public string BlockHash { get; set; }

        public string TxId { get; set; }

        public int Confirmations { get; set; }

        public int State { get; set; }
    }
}
