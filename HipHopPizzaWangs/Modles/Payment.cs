namespace HipHopPizzaWangs.Modles
{
    public class Payment
    {
        public int? Id { get; set; }
        public string? PaymentType { get; set; }
        public List<Order> Order { get; set; }

    }
}
