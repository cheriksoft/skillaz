using System;

namespace UrlShortener.MongoInfrastructure
{
    public class MongoTableAttribute : Attribute
    {
        public string Name { get; set; }

        public MongoTableAttribute(string name)
        {
            Name = name;
        }
    }
}