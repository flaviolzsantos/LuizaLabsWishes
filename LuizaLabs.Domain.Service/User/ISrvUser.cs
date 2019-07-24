using LuizaLabs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LuizaLabs.Domain.Service
{
    public interface ISrvUser
    {
        Task AddUserAsync(User user);
        Task<IEnumerable<User>> GetUserPaginationAsync(int pageSize, int page);
    }
}
