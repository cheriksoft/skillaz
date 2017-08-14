using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace UrlShortener.MongoInfrastructure
{
    public interface IMongoCollectionProvider<in TEntityInterface> where TEntityInterface : class
    {
        IMongoCollection<TEntity> Get<TEntity>() where TEntity : class, TEntityInterface;
    }
}