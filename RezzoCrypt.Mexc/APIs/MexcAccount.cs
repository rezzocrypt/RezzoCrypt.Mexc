using RezzoCrypt.Mexc.Objects.Account;

namespace RezzoCrypt.Mexc.APIs
{
    public class MexcAccount
    {
        private readonly MexcConnection _connection;


        public MexcAccount(MexcConnection connection)
        {
            _connection = connection;
        }

        public AccountInfo AccountInfo() => _connection.GetUrlResult<AccountInfo>("/api/v3/account", secure: true);

        public AccountTrade[] MyTrades(string symbol) => _connection.GetUrlResult<AccountTrade[]>("/api/v3/myTrades", new { symbol, limit = 1000 }, secure: true);
    }
}
