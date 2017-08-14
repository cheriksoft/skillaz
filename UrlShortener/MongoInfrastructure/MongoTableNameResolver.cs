using System.Reflection;

namespace UrlShortener.MongoInfrastructure
{
    public class MongoTableNameResolver : IMongoTableNameResolver
    {
        public string GetEntityTableName<TEntity>()
        {
            var type = typeof(TEntity);

            var attr = type.GetTypeInfo().GetCustomAttribute<MongoTableAttribute>();

            if (attr == null)
            {
                return type.Name;
            }
            return attr.Name;
        }
    }
}