using LuizaLabs.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuizaLabs.Domain.Service
{
    public interface ISrvWish
    {
        Task<IEnumerable<Product>> GetPaginationAsync(int pageSize, int page, string userId);
        Task CreateWishesAsync(List<Wish> listWishProducts, string userId);
    }
}
