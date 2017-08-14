using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using UrlShortener.Entities;
using UrlShortener.MongoInfrastructure;

namespace UrlShortener.Repositories
{
    public class UrlEntryRepository : IUrlEntryRepository
    {
        private readonly IMongoCollectionProvider<IMongoEntity> mongoCollectionProvider;

        public UrlEntryRepository(IMongoCollectionProvider<IMongoEntity> mongoCollectionProvider)
        {
            this.mongoCollectionProvider = mongoCollectionProvider;
        }

        public long GetMaxUrlId()
        {
            if (mongoCollectionProvider.Get<UrlEntry>().AsQueryable().Any())
            {
                return mongoCollectionProvider.Get<UrlEntry>().AsQueryable().Max(e => e.UrlId);
            }

            return 0;

        }

        public async Task<UrlEntry> FindByUrlIdAndIncrementVisitorsCount(long urlId)
        {
            return await mongoCollectionProvider.Get<UrlEntry>().FindOneAndUpdateAsync(
                ue => ue.UrlId.Equals(urlId),
                Builders<UrlEntry>.Update.Inc(ue => ue.VisitorsCount, 1));
        }

        public async Task<IList<UrlEntry>> GetByUserId(Guid userId)
        {
            return await mongoCollectionProvider.Get<UrlEntry>().AsQueryable().Where(ue => ue.UserId == userId)
                .ToListAsync();
        }

        public async Task AddUrlEntry(UrlEntry entry)
        {
            await mongoCollectionProvider.Get<UrlEntry>().InsertOneAsync(entry);
        }
    }
}