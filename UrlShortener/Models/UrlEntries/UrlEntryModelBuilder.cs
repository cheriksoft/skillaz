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
        private readonly ICookieSessionIdProvider cookieSessionIdProvider;
        private readonly IShortUrlFormatter shortUrlFormatter;
        private readonly IUrlEntryRepository urlEntryRepository;
        private readonly IUrlIdStringConverter urlIdStringConverter;

        public UrlEntryModelBuilder(IUrlEntryRepository urlEntryRepository,
            IUrlIdStringConverter urlIdStringConverter,
            ICookieSessionIdProvider cookieSessionIdProvider,
            IShortUrlFormatter shortUrlFormatter)
        {
            this.cookieSessionIdProvider = cookieSessionIdProvider;
            this.shortUrlFormatter = shortUrlFormatter;
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

            return 
                new UrlEntryItemModel(
                    urlId,
                    entry.Url,
                    shortUrlFormatter.Format(urlId),
                    entry.VisitorsCount);
        }

        public async Task<IList<UrlEntryItemModel>> GetForCurrentUser()
        {
            var entries = await urlEntryRepository.GetByUserId(cookieSessionIdProvider.GetUserId());

            return entries.Select(entry =>
                    {
                        var urlIdString = urlIdStringConverter.ConvertFromNumber(entry.UrlId);

                        return new UrlEntryItemModel(
                            urlIdString,
                            entry.Url,
                            shortUrlFormatter.Format(urlIdString),
                            entry.VisitorsCount);
                    }
                )
                .ToList();
        }
    }
}