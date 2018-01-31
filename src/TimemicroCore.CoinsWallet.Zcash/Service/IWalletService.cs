using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Zcash.Service
{
    public interface IWalletService
    {
        /// <summary>
        /// 获取新地址
        /// </summary>
        /// <returns></returns>
        string GetNewAddress();

        /// <summary>
        /// 同步块
        /// </summary>
        void SyncBlock();

        /// <summary>
        /// 同步交易
        /// </summary>
        /// <param name="blockCount"></param>
        void SyncTransaction(int blockCount);

        /// <summary>
        /// 确认交易
        /// </summary>
        void ConfirmTransaction();

        /// <summary>
        /// 确认发送
        /// </summary>
        void ConfirmSend();
    }
}
