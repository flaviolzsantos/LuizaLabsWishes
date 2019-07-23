using LuizaLabs.Domain.Entities;
using LuizaLabs.Infra.Data.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuizaLabs.Infra.Data.Repository
{
    public class RepUser : Repository<User>, IRepUser
    {
        public RepUser(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {

        }
    }
}
