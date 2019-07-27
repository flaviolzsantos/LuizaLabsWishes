using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LuizaLabs.Domain.Service
{
    public interface ISrvBase<T> where T : class
    {       
        Task<List<T>> GetPaginationAsync(int pageSize, int page);
        Task AddAsync(T user);
    }
}
