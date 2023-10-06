namespace HipHopPizzaWangs.Modles
{
    public class User
    {
        public string Id { get; set; }
        public string? CashierEmail { get; set; }
        public string? CashierPassword { get; set; }
        public List<Order> Order { get; set; }

    }
}
