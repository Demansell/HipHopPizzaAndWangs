namespace HipHopPizzaWangs.Modles
{
    public class User
    {
        public int Id { get; set; }
        public string? CashierEmail { get; set; }
        public string? CashierPassword { get; set; }
        public string? Uid { get; set; }
        public List<Order> Order { get; set; }

    }
}
