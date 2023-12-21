namespace RezzoCrypt.Mexc.Objects.Account
{
    public class AccountTrade
    {
        public string OrderId { get; set; }

        public double Price { get; set; }

        public double Qty { get; set; }

        public double QuoteQty { get; set; }
        public double Commission { get; set; }
        public string CommissionAsset { get; set; }

        public bool IsBuyer { get; set; }

    }
}
