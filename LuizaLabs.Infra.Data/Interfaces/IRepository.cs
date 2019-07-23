using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LuizaLabs.Infra.Data.Interfaces
{
    public interface IRepository<T>  where T : class
    {
        Task<T> GetById(Guid id);
        Task Add(T obj);
        Task<IEnumerable<T>> GetAll();
        Task Update(T obj, Guid id);
    }
}
