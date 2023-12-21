using System.Linq;

namespace RezzoCrypt.Mexc.Objects.Exchange
{
    public class Order
    {
        public string OrderId { get; set; }
        public string Symbol { get; set; }
        public double Price { get; set; }
        public double OrigQty { get; set; }
        public double OrigQuoteOrderQty { get; set; }
        public double executedQty { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Side { get; set; }
        public string IsWorking { get; set; }
        public string Time { get; set; }
        public string UpdateTime { get; set; }
    }
}
