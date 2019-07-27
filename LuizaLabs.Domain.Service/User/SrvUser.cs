using LuizaLabs.Domain.Entities;
using LuizaLabs.Infra.Cross;
using LuizaLabs.Infra.Data.Interfaces;
using LuizaLabs.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
            await _repUser.Add(user);
        }

        public async Task<List<User>> GetPaginationAsync(int pageSize, int page)
        {
            if (page <= 0 || pageSize < 0)
                throw new ValidationException("Páginação incorreta");


            return await _repUser.GetPaginationAsync(pageSize, page);
        }


    }
}
