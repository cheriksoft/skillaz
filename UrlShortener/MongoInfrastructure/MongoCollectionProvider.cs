
using System;
using System.Collections.Generic;
using System.Reflection;
using MongoDB.Driver;
using UrlShortener.Entities;

namespace UrlShortener.MongoInfrastructure
{
    public class MongoCollectionProvider<TEntityInterface> : IMongoCollectionProvider<TEntityInterface> where TEntityInterface : class
    {
        private readonly IMongoDbProvider mongoDbProvider;
        private readonly IMongoTableNameResolver mongoTableNameResolver;

        public MongoCollectionProvider(IMongoDbProvider mongoDbProvider,
            IMongoTableNameResolver mongoTableNameResolver)
        {
            this.mongoDbProvider = mongoDbProvider;
            this.mongoTableNameResolver = mongoTableNameResolver;
        }

        public IMongoCollection<TEntity> Get<TEntity>() where TEntity : class, TEntityInterface
        {
            return mongoDbProvider.Get().GetCollection<TEntity>(mongoTableNameResolver.GetEntityTableName<TEntity>());
        }
    }
}