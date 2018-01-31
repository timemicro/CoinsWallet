using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Litecoin.PO
{
    public class ReceiveAddressPO
    {
        public string Address { get; set; }

        public string PrivateKey { get; set; }

        public decimal TotalReceived { get; set; }
    }
}
