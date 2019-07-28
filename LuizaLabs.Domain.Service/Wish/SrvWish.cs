using System.Collections.Generic;
using System.Threading.Tasks;
using LuizaLabs.Domain.Entities;

namespace LuizaLabs.Domain.Service
{
    public class SrvWish : ISrvWish
    {
        private readonly ISrvUser _srvUser;
        private readonly ISrvProduct _srvProduct;

        public SrvWish(ISrvUser srvUser, ISrvProduct srvProduct)
        {
            _srvUser = srvUser;
            _srvProduct = srvProduct;
        }

        public async Task CreateWishesAsync(List<Wish> listWishProducts, string userId)
        {
            User user = await _srvUser.GetUserAsync(userId);
            user.AddWishes(await _srvProduct.GetProductAsync(listWishProducts));
            await _srvUser.UpdateUserAsync(user);
        }

        public async Task<IEnumerable<Product>> GetPaginationAsync(int pageSize, int page, string userId)
        {
            User user = await _srvUser.GetUserAsync(userId);
            user.ValidateWishes();
            return user.GetWishesPagination(pageSize, page);
        }

    }
}
