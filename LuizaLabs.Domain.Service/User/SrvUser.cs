using LuizaLabs.Domain.Entities;
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

        public async Task AddUserAsync()
        {
            User user = new User() { Email = "teste", Id = Guid.NewGuid(), Name = "Nome" };
            await _repUser.Add(user);
        }


    }
}
