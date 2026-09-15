using Newtonsoft.Json;

namespace RezzoCrypt.Mexc.Objects.Exchange
{
    public class Order
    {
        public string? OrderId { get; set; }
        public string? Symbol { get; set; }
        public double? Price { get; set; }
        public double? OrigQty { get; set; }
        public double? OrigQuoteOrderQty { get; set; }

        [JsonProperty("executedQty")]
        public double? ExecutedQty { get; set; }

        public string? Status { get; set; }
        public string? Type { get; set; }
        public string? Side { get; set; }
        public bool? IsWorking { get; set; }
        public long? Time { get; set; }
        public long? UpdateTime { get; set; }
    }
}