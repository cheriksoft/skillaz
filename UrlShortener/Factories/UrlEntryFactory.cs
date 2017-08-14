using System;
using UrlShortener.Entities;

namespace UrlShortener.Factories
{
    public class UrlEntryFactory : IUrlEntryFactory
    {
        public UrlEntry Create(long urlId, string url, Guid userId)
        {
            return new UrlEntry()
            {
                UrlId = urlId,
                UserId = userId,
                Url = url,
                VisitorsCount = 0
            };
        }
    }
}