namespace HipHopPizzaWangs.Modles
{
    public class Orders
    {
        public int? Id { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerPhoneNumber { get; set; }
        public string? UserId { get; set;}
        public List <User> Users { get; set; }
        public string? PaymentTypeId { get; set;}
        public List <Payments> Payments { get; set; }
        public bool? IsOpen { get; set; }
        public int OrderTotal { get; set; }
        public string OrderType { get; set; }
        public bool Feedback { get; set; }
        public int Tip { get; set; }

    }
}
