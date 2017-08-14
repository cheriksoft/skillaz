namespace UrlShortener.MongoInfrastructure
{
    public interface IMongoTableNameResolver
    {
        string GetEntityTableName<TEntity>();
    }
}