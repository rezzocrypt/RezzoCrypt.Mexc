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

        public Task<AccountInfo> AccountInfoAsync() => _connection.GetUrlResultAsync<AccountInfo>("/api/v3/account", secure: true);

        public Task<AccountTrade[]> MyTradesAsync(string symbol) => _connection.GetUrlResultAsync<AccountTrade[]>("/api/v3/myTrades", new { symbol, limit = 1000 }, secure: true);
    }
}