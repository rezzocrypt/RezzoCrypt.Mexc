using System.Linq;

namespace RezzoCrypt.Mexc.Objects.Exchange
{
    public class BidsAsks
    {
        public long LastUpdateId { get; set; }

        public double[][] Bids { get; set; }
        public double[][] Asks { get; set; }
    }
}
