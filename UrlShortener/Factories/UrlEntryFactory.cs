using System;
using UrlShortener.Entities;

namespace UrlShortener.Factories
{
    public class UrlEntryFactory : IUrlEntryFactory
    {
        public UrlEntry Create(long urlId, string url, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}