namespace Common
{
    public class UserPreference
    {
        public Guid PreferenceId { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int OrderQuantity { get; set; }
        public string AccountNumber { get; set; }
        public int TotalAmount { get; set; }
        public decimal TotalFee { get; set; }
    }
}
