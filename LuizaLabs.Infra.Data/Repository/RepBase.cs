using LuizaLabs.Infra.Data.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LuizaLabs.Infra.Data.Repository
{
    public abstract class RepBase<T> : IRepository<T> where T : class
    {
        protected readonly IMongoDatabase _mongoDatabase;
        protected readonly IMongoCollection<T> _dbSet;

        protected RepBase(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
            _dbSet = _mongoDatabase.GetCollection<T>(typeof(T).Name);
        }

        public async Task<T> GetById(Guid id)
        {
            var data = await _dbSet.FindAsync(Builders<T>.Filter.Eq("_id", id));
            return data.FirstOrDefault();
        }

        public Task Add(T obj)
        {
            return _dbSet.InsertOneAsync(obj);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var all = await _dbSet.FindAsync(Builders<T>.Filter.Empty);
            return all.ToList();
        }

        public Task Update(T obj, Guid id)
        {
            return  _dbSet.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", id), obj);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
