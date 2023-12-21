using RezzoCrypt.Mexc.Extensions;
using RezzoCrypt.Mexc.Objects.Data;
using System;

namespace RezzoCrypt.Mexc.APIs
{
    public class MexcData
    {
        private MexcConnection _connection;
        public MexcData(MexcConnection connection)
        {
            _connection = connection;
        }

        public Trades[] Trades(string symbol) => _connection.GetUrlResult<Trades[]>("/api/v3/trades", new { symbol, limit = 1000 });

        public double[][] Kline(string symbol, DateTime startDate, DateTime endDate) => _connection.GetUrlResult<double[][]>("/api/v3/klines", new { symbol, interval = "5m", limit = 1000, startTime = startDate.StringTicksFromDate(), endTime = endDate.StringTicksFromDate() });
    }
}
