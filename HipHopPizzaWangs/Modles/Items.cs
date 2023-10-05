namespace HipHopPizzaWangs.Modles
{
    public class Items
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? orderId { get; set; }
        public List<Orders> Orders { get; set; }
    }
}
