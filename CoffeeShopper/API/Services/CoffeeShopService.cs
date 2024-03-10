using API.Dtos;
using API.Extensions;
using DB.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class CoffeeShopService : ICoffeeShopService
    {
        private readonly AppDbContext context;

        public CoffeeShopService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<CoffeeShopDto>> List()
        {
            return await context.CoffeeShops
                .ToCoffeeShopDto()
                .ToListAsync();
        }
    }
}
