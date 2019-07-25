using LuizaLabs.Domain.Entities;
using LuizaLabs.Infra.Data.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LuizaLabs.Infra.Data.Repository
{
    public class RepUser : Repository<User>, IRepUser
    {
        private new readonly IMongoDatabase _mongoDatabase;
        public RepUser(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

       
    }
}
