namespace API.Dtos
{
    public class CoffeeShopDto
    {
        public int Id { get; }

        public string Name { get; set; } = null!;

        public string OpeningHours { get; set; } = null!;

        public string Address { get; set; } = null!;
    }
}
