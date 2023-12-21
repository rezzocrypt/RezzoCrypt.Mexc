using Newtonsoft.Json;

namespace RezzoCrypt.Mexc.Objects.Data
{
    public class Trades
    {
        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }
        [JsonProperty(PropertyName = "time")]
        public string Date { get; set; }
    }
}
