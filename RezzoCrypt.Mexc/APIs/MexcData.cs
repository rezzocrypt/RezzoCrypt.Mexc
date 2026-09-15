using RezzoCrypt.Mexc.Extensions;
using RezzoCrypt.Mexc.Objects.Data;

namespace RezzoCrypt.Mexc.APIs
{
    public class MexcData
    {
        private MexcConnection _connection;

        public MexcData(MexcConnection connection)
        {
            _connection = connection;
        }

        public Task<Trades[]> TradesAsync(string symbol) => _connection.GetUrlResultAsync<Trades[]>("/api/v3/trades", new { symbol, limit = 1000 });

        public Task<double[][]> KlineAsync(string symbol, DateTime startDate, DateTime endDate, string interval = "5m") => _connection.GetUrlResultAsync<double[][]>("/api/v3/klines", new { symbol, interval, limit = 1000, startTime = startDate.StringTicksFromDate(), endTime = endDate.StringTicksFromDate() });
    }
}