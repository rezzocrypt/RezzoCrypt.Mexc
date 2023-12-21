namespace RezzoCrypt.Mexc.Objects.Account
{
    public class AccountInfo
    {
        public class AccountBalances
        {
            public string Asset { get; set; }
            public double Free { get; set; }
            public double Locked { get; set; }

            public double Amount => Free + Locked;
        }

        public int MakerCommission { get; set; }
        public int TakerCommission { get; set; }
        public int BuyerCommission { get; set; }
        public int SellerCommission { get; set; }

        public bool CanTrade { get; set; }
        public bool CanWithdraw { get; set; }
        public bool CanDeposit { get; set; }

        public string AccountType { get; set; }
        public string[] Permissions { get; set; }

        public AccountBalances[] Balances { get; set; }
    }
}
