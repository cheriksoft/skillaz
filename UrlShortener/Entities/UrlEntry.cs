using System;
using MongoDB.Bson;
using UrlShortener.MongoInfrastructure;

namespace UrlShortener.Entities
{
    [MongoTable("UrlEntries")]
    public class UrlEntry : IMongoEntity
    {
        public ObjectId Id { get; set; }

        public long UrlId { get; set; }

        public string Url { get; set; }

        public int VisitorsCount { get; set; }

        public Guid UserId { get; set; }
    }
}