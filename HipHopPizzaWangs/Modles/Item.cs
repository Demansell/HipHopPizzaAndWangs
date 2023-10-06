namespace HipHopPizzaWangs.Modles
{
    public class Item
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? OrderId { get; set; }
        public List<Order> Order { get; set; }
    }
}
