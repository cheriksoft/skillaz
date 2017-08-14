using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShortener.Entities;

namespace UrlShortener.Repositories
{
    public interface IUrlEntryRepository
    {
        long GetMaxUrlId();
        Task<UrlEntry> FindByUrlIdAndIncrementVisitorsCount(long urlId);
        Task<IList<UrlEntry>> GetByUserId(Guid userId);
        Task AddUrlEntry(UrlEntry entry);
    }
}