using LuizaLabs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LuizaLabs.Domain.Service
{
    public interface ISrvUser : ISrvBase<User>
    {
        Task<User> GetUserAsync(string userId);
        Task UpdateUserAsync(User user);
    }
}
