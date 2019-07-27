using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LuizaLabs.Domain.Entities;
using System.Linq;
using LuizaLabs.Infra.Cross;
using LuizaLabs.Infra.Data.Interfaces;

namespace LuizaLabs.Domain.Service
{
    public class SrvWish : ISrvWish
    {
        private readonly IRepWish _repWish;
        private readonly ISrvUser _srvUser;
        private readonly ISrvProduct _srvProduct;

        public SrvWish(IRepWish repWish, ISrvUser srvUser, ISrvProduct srvProduct)
        {
            _repWish = repWish;
            _srvUser = srvUser;
            _srvProduct = srvProduct;
        }

        public async Task CreateWishesAsync(List<Wish> listWishProducts, string userId)
        {
            User user = await _srvUser.GetUserAsync(userId);
            user.Wishes = await _srvProduct.GetProductAsync(listWishProducts);
            await _srvUser.UpdateUserAsync(user);
        }

        public async Task<IEnumerable<Product>> GetPaginationAsync(int pageSize, int page, string userId)
        {
            if (page <= 0 || pageSize < 0)
                throw new ValidationException("Páginação incorreta");

            User user = await _srvUser.GetUserAsync(userId);

            if (user.Wishes == null || !user.Wishes.Any())
                throw new NotFoundException("Lista de desejo não encontrada");

            IEnumerable<Product> listProducts = user.Wishes.Skip(pageSize * (page-1)).Take(pageSize);

            if (!listProducts.Any())
                throw new NotFoundException("Lista de desejo não encontrada");

            return listProducts;
        }

    }
}
