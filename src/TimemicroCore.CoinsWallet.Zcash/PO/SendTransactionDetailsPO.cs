﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Zcash.PO
{
    public class SendTransactionDetailsPO
    {
        public string TxId { get; set; }

        public string Address { get; set; }

        public decimal Amount { get; set; }
    }
}
