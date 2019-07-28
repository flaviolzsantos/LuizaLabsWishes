using LuizaLabs.Domain.Entities;

namespace LuizaLabs.Domain.Service
{
    public interface ISrvAuthentication
    {
        object Authentication(Auth auth);
    }
}
