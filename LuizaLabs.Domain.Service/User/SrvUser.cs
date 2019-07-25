using LuizaLabs.Domain.Entities;
using LuizaLabs.Infra.Cross;
using LuizaLabs.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LuizaLabs.Domain.Service
{
    public class SrvUser : ISrvUser
    {
        private readonly RepUser _repUser;
        public SrvUser(RepUser repUser)
        {
            _repUser = repUser;
        }

        public async Task AddUserAsync(User user)
        {
            await _repUser.Add(user);
        }

        public async Task<List<User>> GetUserPaginationAsync(int pageSize, int page)
        {
            if (page <= 0 || pageSize < 0)
                throw new ValidationException("Páginação incorreta");


            return await _repUser.GetPaginationAsync(pageSize, page);
        }


    }
}
