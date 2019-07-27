using LuizaLabs.Infra.Data.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using LuizaLabs.Infra.Cross;
using MongoDB.Bson.Serialization;
using System.Reflection;

namespace LuizaLabs.Infra.Data.Repository
{
    public abstract class Repository<T> where T : class
    {
        protected readonly IMongoDatabase _mongoDatabase;
        protected readonly IMongoCollection<T> _dbSet;

        protected Repository(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
            _dbSet = _mongoDatabase.GetCollection<T>(typeof(T).Name);
        }

        public async Task<T> GetById(Guid id) => (await FindAsync("_id", id));

        public Task Add(T obj) => _dbSet.InsertOneAsync(obj);

        public async Task<IEnumerable<T>> GetAll() => (await _dbSet.FindAsync(Builders<T>.Filter.Empty)).ToList();

        public Task Update(T obj, Guid id) => _dbSet.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", id), obj);

        public virtual Task Remove(Guid id) => _dbSet.DeleteOneAsync(Builders<T>.Filter.Eq("_id", id));

        public async Task<List<T>> GetPaginationAsync(int pageSize, int page, string[] fieldsToReturn = null)
        {
            if(fieldsToReturn == null)
            {
                PropertyInfo[] props = typeof(T).GetProperties();
                fieldsToReturn = props.Select(x => x.Name).ToArray();
            }

            var projectionBuilder = Builders<T>.Projection;
            var projection = projectionBuilder.Combine(fieldsToReturn?.Select(field => projectionBuilder.Include(field)));

            List<T> list = new List<T>();
            await _mongoDatabase.GetCollection<T>(typeof(T).Name).Find(FilterDefinition<T>.Empty)
                .Project(projection)
                .Skip((pageSize * (page - 1)))
                .Limit(pageSize).ForEachAsync(x => list.Add(BsonSerializer.Deserialize<T>(x)));           

            return list;
        }

        private async Task<T> FindAsync(string property, object value) => (await _dbSet.FindAsync(Builders<T>.Filter.Eq(property, value))).FirstOrDefault();

        public async Task<bool> HasName(string name) => (await FindAsync("Name", name)) != null;


    }
}
