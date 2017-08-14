using System;
using MongoDB.Driver;
using UrlShortener.Entities;

namespace UrlShortener.Factories
{
    public interface IUrlEntryFactory
    {
        UrlEntry Create(long urlId, string url, Guid userId);
    }
}