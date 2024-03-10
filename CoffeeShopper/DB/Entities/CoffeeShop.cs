namespace DB.Entities
{
    public class CoffeeShop
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string OpeningHours { get; set; } = null!;

        public string Address { get; set; } = null!;
    }
}
