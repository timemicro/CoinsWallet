using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Bitcoin.PO
{
    public class BlockPO
    {
        public string Hash { get; set; }

        public int Height { get; set; }

        public int State { get; set; }
    }
}
