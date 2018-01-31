using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Litecoin.PO
{
    public class SendRequestPO
    {
        public long Id { get; set; }

        public string OutRequestNo { get; set; }

        public string Address { get; set; }

        public decimal Amount { get; set; }

        public int State { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
