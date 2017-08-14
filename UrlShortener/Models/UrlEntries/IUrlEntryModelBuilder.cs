using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UrlShortener.Models.UrlEntries
{
    public interface IUrlEntryModelBuilder
    {
        Task<UrlEntryItemModel> GetByStringUrlId(string urlId);
        Task<IList<UrlEntryItemModel>> GetForCurrentUser();
    }
}