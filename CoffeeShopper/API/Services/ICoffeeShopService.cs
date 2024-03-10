using API.Dtos;

namespace API.Services
{
    public interface ICoffeeShopService
    {
        Task<IEnumerable<CoffeeShopDto>> List();
    }
}
