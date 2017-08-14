using MongoDB.Driver;

namespace UrlShortener.MongoInfrastructure
{
    public interface IMongoDbProvider
    {
        IMongoDatabase Get();
    }
}