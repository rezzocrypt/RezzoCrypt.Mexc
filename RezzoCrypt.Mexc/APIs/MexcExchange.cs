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
            LIMIT_MAKER
        }

        public MexcExchange(MexcConnection connection)
        {
            _connection = connection;
        }

        public BidsAsks ExchangePositions(string symbol, int limit = 10) => _connection.GetUrlResult<BidsAsks>("/api/v3/depth", new { symbol, limit });

        public Order[] OpenedOrders(string symbol) => _connection.GetUrlResult<Order[]>("/api/v3/openOrders", new { symbol }, secure: true);

        public Order[] AllOrders(string symbol, DateTime startDate, DateTime endDate) => _connection.GetUrlResult<Order[]>("/api/v3/allOrders", new { symbol, startTime = startDate.StringTicksFromDate(), endTime = endDate.StringTicksFromDate() }, secure: true);

        public string CancellAllOrders(string symbol) => _connection.GetUrlResult<string>("/api/v3/openOrders", new { symbol }, MexcConnection.Method.Delete, secure: true);

        public string CancelOrder(string symbol, string orderId) => _connection.GetUrlResult<string>("/api/v3/order", new { symbol, orderId }, MexcConnection.Method.Delete, secure: true);

        public Order PlaceOrder(string symbol, double price, double qty, OrderSide side = OrderSide.BUY, OrderType type = OrderType.LIMIT) => _connection.GetUrlResult<Order>("/api/v3/order", new
        {
            symbol,
            side = side.ToString(),
            type = type.ToString(),
            quantity = qty,
            price
        }, MexcConnection.Method.Post, secure: true);

        public Order PlaceMarketOrder(string symbol, double qty, OrderSide side = OrderSide.BUY) => _connection.GetUrlResult<Order>("/api/v3/order", new
        {
            symbol,
            side = side.ToString(),
            type = OrderType.MARKET.ToString(),
            quantity = qty
        }, MexcConnection.Method.Post, secure: true);
    }
}
