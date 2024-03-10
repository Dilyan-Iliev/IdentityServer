using API.Dtos;
using DB.Entities;

namespace API.Extensions
{
    public static class DtoExtensions
    {
        public static IQueryable<CoffeeShopDto> ToCoffeeShopDto(this IQueryable<CoffeeShop> query)
        {
            return query.Select(cs => new CoffeeShopDto
            {
                Address = cs.Address,
                Name = cs.Name,
                OpeningHours = cs.OpeningHours,
            });
        }
    }
}
