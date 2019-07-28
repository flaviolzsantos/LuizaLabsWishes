using LuizaLabs.Infra.Cross;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LuizaLabs.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public List<Product> Wishes { get; private set; }

        public void ValidateWishes()
        {
            if (Wishes == null || Wishes.Any())
                throw new NotFoundException("Lista de desejo não encontrada");
        }

        public void ValidateUser()
        {
            if (string.IsNullOrEmpty(Name))
                throw new ValidationException("Nome requerido");

            if (string.IsNullOrEmpty(Email))
                throw new ValidationException("Email requerido");
        }

        public IEnumerable<Product> GetWishesPagination(int pageSize, int page)
        {
            if (page <= 0 || pageSize < 0)
                throw new ValidationException("Páginação incorreta");

            IEnumerable<Product> listProducts = Wishes.Skip(pageSize * (page - 1)).Take(pageSize);

            if (!listProducts.Any())
                throw new NotFoundException("Lista de desejo não encontrada");

            return listProducts;
        }

        public void AddWishes(List<Product> products)
        {
            if (products == null)
                throw new ValidationException("Não existe lista de desejo para adiciona ao usuário");

            Wishes = products;
        }
    }
}
