namespace HipHopPizzaWangs.Modles
{
    public class Order
    {
        public int? Id { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerPhoneNumber { get; set; }
        public string? UserId { get; set;}
        public User? User { get; set; }
        public int? PaymentTypeId { get; set;}
        public Payment? Payment { get; set; }
        public bool? IsOpen { get; set; }
        public int OrderTotal { get; set; }
        public string? OrderType { get; set; }
        public bool Feedback { get; set; }
        public int Tip { get; set; }
        public int ItemId { get; set; }
        public List<Item>? Items { get; set; }
    }
}
