using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.Kestrel;
using UrlShortener.Entities;
using UrlShortener.Infrastructure;
using UrlShortener.Repositories;

namespace UrlShortener.Models.UrlEntries
{
    public class UrlEntryModelBuilder : IUrlEntryModelBuilder
    {
        public ICookieSessionIdProvider CookieSessionIdProvider { get; }
        private readonly IUrlEntryRepository urlEntryRepository;
        private readonly IUrlIdStringConverter urlIdStringConverter;

        public UrlEntryModelBuilder(IUrlEntryRepository urlEntryRepository,
            IUrlIdStringConverter urlIdStringConverter,
            ICookieSessionIdProvider cookieSessionIdProvider)
        {
            CookieSessionIdProvider = cookieSessionIdProvider;
            this.urlEntryRepository = urlEntryRepository;
            this.urlIdStringConverter = urlIdStringConverter;
        }

        public async Task<UrlEntryItemModel> GetByStringUrlId(string urlId)
        {
            var urlIdLong = urlIdStringConverter.ConvertFromString(urlId);

            var entry = await urlEntryRepository.FindByUrlIdAndIncrementVisitorsCount(urlIdLong);

            if (entry == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound);
            }

            return new UrlEntryItemModel(urlIdStringConverter.ConvertFromNumber(entry.UrlId), entry.Url, entry.VisitorsCount);
        }

        public async Task<IList<UrlEntryItemModel>> GetForCurrentUser()
        {
            var entries = await urlEntryRepository.GetByUserId(CookieSessionIdProvider.GetUserId());

            return entries.Select(entry => new UrlEntryItemModel(urlIdStringConverter.ConvertFromNumber(entry.UrlId),
                entry.Url, entry.VisitorsCount)).ToList();
        }
    }
}