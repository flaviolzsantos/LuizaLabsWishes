using LuizaLabs.Domain.Entities;
using LuizaLabs.Infra.Data.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuizaLabs.Infra.Data.Repository
{
    public class RepProduct : Repository<Product>, IRepProduct
    {
        public RepProduct(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {

        }
    }
}
