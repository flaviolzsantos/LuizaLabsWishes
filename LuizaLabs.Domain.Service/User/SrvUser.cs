using LuizaLabs.Domain.Entities;
using LuizaLabs.Infra.Cross;
using LuizaLabs.Infra.Data.Interfaces;
using LuizaLabs.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace LuizaLabs.Domain.Service
{
    public class SrvUser : ISrvUser
    {
        private readonly IRepUser _repUser;
        public SrvUser(IRepUser repUser)
        {
            _repUser = repUser;
        }

        public async Task AddAsync(User user)
        {
            if (user == null)
                throw new NotContentException(string.Empty);

            user.ValidateUser();

            if (await _repUser.HasName(user.Name))
                throw new AlreadyExistException("Não foi possível cadastrar esse usuário pois ele já está cadastrado");

            await _repUser.Add(user);
        }

        public async Task<List<User>> GetPaginationAsync(int pageSize, int page)
        {
            if (page <= 0 || pageSize < 0)
                throw new ValidationException("Páginação incorreta");

            var list = await _repUser.GetPaginationAsync(pageSize, page, new string[] { "Id", "Name", "Email" });

            if (!list.Any())
                throw new NotFoundException("Lista de usuário não encontrado");

            return list;
        }

        public async Task<User> GetUserAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new NotContentException(string.Empty);

            Guid guid;
            if (!Guid.TryParse(userId, out guid))
                throw new ValidationException("Usuário inválido");

            User user = await _repUser.GetById(guid);

            if (user == null)
                throw new NotFoundException("Usuário não encontrado");

            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            await _repUser.Update(user, user.Id);
        }


    }
}
