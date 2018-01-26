using System;

namespace TimemicroCore.CoinsWallet.Api
{
    public interface IApiService
    {
        string Name { get; }

        Type ReqType { get; }

        object Execute(object req);
    }
}
