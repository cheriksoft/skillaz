using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UrlShortener.Entities;

namespace UrlShortener.MongoInfrastructure
{
    public class MongoDbProvider : IMongoDbProvider
    {
        private readonly IOptions<MongoDbConfiguration> options;
        private readonly IMongoTableNameResolver mongoTableNameResolver;
        private readonly IMongoClient mongoClient;
        private readonly IMongoDatabase mongoDatabase;

        public MongoDbProvider(IOptions<MongoDbConfiguration> options,
            IMongoTableNameResolver mongoTableNameResolver)
        {
            this.options = options;
            this.mongoTableNameResolver = mongoTableNameResolver;
            mongoClient = new MongoClient(options.Value.Server);
            mongoDatabase = mongoClient.GetDatabase(options.Value.Database);

            var collection =
                mongoDatabase.GetCollection<UrlEntry>(mongoTableNameResolver.GetEntityTableName<UrlEntry>());

            collection.Indexes.CreateMany(new CreateIndexModel<UrlEntry>[]
            {
                new CreateIndexModel<UrlEntry>(Builders<UrlEntry>.IndexKeys.Ascending(ue => ue.UrlId)),
                new CreateIndexModel<UrlEntry>(Builders<UrlEntry>.IndexKeys.Ascending(ue => ue.UserId)),
            });
        }

        public IMongoDatabase Get()
        {
            return mongoDatabase;
        }
    }
}