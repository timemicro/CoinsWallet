using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Bitcoin.PO
{
    public class SendRequestPO
    {
        public string Address { get; set; }

        public decimal Amount { get; set; }

        public int State { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
