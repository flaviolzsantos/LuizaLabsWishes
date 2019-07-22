using System;
using System.Collections.Generic;
using System.Text;

namespace LuizaLabs.Infra.Data.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {

    }
}
