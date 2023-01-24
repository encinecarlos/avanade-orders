using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using orders.Api.Settings;
using Orders.Api.Domain.Interfaces;

namespace orders.Api.Domain.Services
{
    public class MongoDbServiceHandler<T, TId> : IMongoDbServiceHandler<T, TId>
    {
        public ILogger<MongoDbServiceHandler<T, TId>> Logger { get; set; }
        public MongoClient Client { get; set; }
        public IMongoCollection<T> MongoCollection { get; set; }

        public MongoDbServiceHandler(
            ILogger<MongoDbServiceHandler<T, TId>> logger,
            IOptions<MongodbSettings> config)
        {
            Logger = logger;
            var mongoUrl = new MongoUrl(config.Value.Connection);
            var collectionName = typeof(T).Name;

            var mongoSettings = MongoClientSettings.FromUrl(mongoUrl);
            Client = new MongoClient(mongoSettings);

            MongoCollection = Client.GetDatabase(config.Value.Database).GetCollection<T>(collectionName);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            Logger.LogInformation("Get all data from collection");
            return await (await MongoCollection.FindAsync(predicate)).ToListAsync();
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            Logger.LogInformation("Get single record from collection");
            return await (await MongoCollection.FindAsync(predicate)).FirstOrDefaultAsync();
        }

        public async Task InsertAsync(T entity)
        {
            Logger.LogInformation("Insert data on collection");
            await MongoCollection.InsertOneAsync(entity);
        }

        public async Task RemoveAsync(TId id)
        {
            Logger.LogInformation("Remove document from collection");
            var filter = Builders<T>.Filter.Eq("_id", id);
            await MongoCollection.DeleteOneAsync(filter);
        }

        public async Task<ReplaceOneResult> UpdateAsync(TId id, T entity)
        {
            Logger.LogInformation("Update document on collection");
            var filter = Builders<T>.Filter.Eq("_id", id);
            return await MongoCollection.ReplaceOneAsync(filter, entity);
        }
    }
}