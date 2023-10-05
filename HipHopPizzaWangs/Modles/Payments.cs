namespace HipHopPizzaWangs.Modles
{
    public class Payments
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public List<Orders> Orders { get; set; }

    }
}
