using System.Linq;
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
            return mongoCollectionProvider.Get<UrlEntry>().AsQueryable().Select(ue => ue.UrlId).DefaultIfEmpty(0).Max();
        }
    }
}