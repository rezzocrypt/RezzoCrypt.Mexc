using RezzoCrypt.Mexc.Extensions;
using RezzoCrypt.Mexc.Objects.Exchange;

namespace RezzoCrypt.Mexc.APIs
{
    public class MexcExchange
    {
        private readonly MexcConnection _connection;

        public enum OrderSide
        {
            BUY,
            SELL
        }

        public enum OrderType
        {
            LIMIT,
            MARKET,
            LIMIT_MAKER,
            IOC,
            FOK
        }

        public MexcExchange(MexcConnection connection)
        {
            _connection = connection;
        }

        public Task<BidsAsks> ExchangePositionsAsync(string symbol, int limit = 10) => _connection.GetUrlResultAsync<BidsAsks>("/api/v3/depth", new { symbol, limit });

        public Task<Order[]> OpenedOrdersAsync(string symbol) => _connection.GetUrlResultAsync<Order[]>("/api/v3/openOrders", new { symbol }, secure: true);

        public Task<Order[]> AllOrdersAsync(string symbol, DateTime startDate, DateTime endDate) => _connection.GetUrlResultAsync<Order[]>("/api/v3/allOrders", new { symbol, startTime = startDate.StringTicksFromDate(), endTime = endDate.StringTicksFromDate() }, secure: true);

        public Task<string> CancelAllOrdersAsync(string symbol) => _connection.GetUrlResultAsync<string>("/api/v3/openOrders", new { symbol }, MexcConnection.Method.Delete, secure: true);

        public Task<string> CancelOrderAsync(string symbol, string orderId) => _connection.GetUrlResultAsync<string>("/api/v3/order", new { symbol, orderId }, MexcConnection.Method.Delete, secure: true);

        public Task<Order> PlaceOrderAsync(string symbol, double price, double qty, OrderSide side = OrderSide.BUY, OrderType type = OrderType.LIMIT) => _connection.GetUrlResultAsync<Order>("/api/v3/order", new
        {
            symbol,
            side = side.ToString(),
            type = type.ToString(),
            quantity = qty,
            price
        }, MexcConnection.Method.Post, secure: true);

        public Task<Order> PlaceMarketOrderAsync(string symbol, double qty, OrderSide side = OrderSide.BUY) => _connection.GetUrlResultAsync<Order>("/api/v3/order", new
        {
            symbol,
            side = side.ToString(),
            type = OrderType.MARKET.ToString(),
            quantity = qty
        }, MexcConnection.Method.Post, secure: true);
    }
}