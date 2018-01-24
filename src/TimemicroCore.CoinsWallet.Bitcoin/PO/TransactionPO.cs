using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Bitcoin.PO
{
    public class TransactionPO
    {
        public BlockPO Block { get; set; }

        public string TxId { get; set; }

        public int Confirmations { get; set; }
    }
}
