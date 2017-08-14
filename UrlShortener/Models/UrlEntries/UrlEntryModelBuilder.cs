using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Entities;
using UrlShortener.Infrastructure;
using UrlShortener.Repositories;

namespace UrlShortener.Models.UrlEntries
{
    public class UrlEntryModelBuilder : IUrlEntryModelBuilder
    {
        private readonly IUrlEntryRepository urlEntryRepository;
        private readonly IUrlIdStringConverter urlIdStringConverter;

        public UrlEntryModelBuilder(IUrlEntryRepository urlEntryRepository,
            IUrlIdStringConverter urlIdStringConverter)
        {
            this.urlEntryRepository = urlEntryRepository;
            this.urlIdStringConverter = urlIdStringConverter;
        }

        public async Task<UrlEntryItemModel> GetByStringUrlId(string urlId)
        {
            var urlIdLong = urlIdStringConverter.ConvertFromString(urlId);

            var entry = await urlEntryRepository.FindByUrlIdAndIncrementVisitorsCount(urlIdLong);

            if (entry == null) return null;

            return new UrlEntryItemModel(urlIdStringConverter.ConvertFromNumber(entry.UrlId), entry.Url, entry.VisitorsCount);
        }

        public async Task<IList<UrlEntryItemModel>> GetByUserId(Guid userId)
        {
            var entries = await urlEntryRepository.GetByUserId(userId);

            return Enumerable.Select<UrlEntry, UrlEntryItemModel>(entries, entry => new UrlEntryItemModel(urlIdStringConverter.ConvertFromNumber(entry.UrlId),
                entry.Url, entry.VisitorsCount)).ToList();
        }
    }
}