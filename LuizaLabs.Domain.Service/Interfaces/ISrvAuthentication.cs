using LuizaLabs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuizaLabs.Domain.Service
{
    public interface ISrvAuthentication
    {
        object Authentication(Auth auth);
    }
}
