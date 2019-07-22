using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuizaLabs.Infra.Data.Connection
{
    public class ConnectionMongo
    {
        private MongoClient _mongoClient;
        public ConnectionMongo(string strConnection)
        {
            _mongoClient = new MongoClient(strConnection);
        }

        public IMongoDatabase GetMongoDatabase(string strDataBase)
        {
            return _mongoClient.GetDatabase(strDataBase);
        }
    }
}
