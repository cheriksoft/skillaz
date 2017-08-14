using System.Net;
using System.Threading.Tasks;
using UrlShortener.Factories;
using UrlShortener.Infrastructure;
using UrlShortener.Repositories;

namespace UrlShortener.Models.UrlEntries
{
    public class UrlEntryHandler : IUrlEntryHandler
    {
        private readonly IUrlEntryRepository urlEntryRepository;
        private readonly IUrlIdGenerator urlIdGenerator;
        private readonly IUrlIdStringConverter urlIdStringConverter;
        private readonly IUrlEntryFactory urlEntryFactory;
        private readonly ICookieSessionIdProvider cookieSessionIdProvider;

        public UrlEntryHandler(IUrlEntryRepository urlEntryRepository,
            IUrlIdGenerator urlIdGenerator,
            IUrlIdStringConverter urlIdStringConverter,
            IUrlEntryFactory urlEntryFactory,
            ICookieSessionIdProvider cookieSessionIdProvider)
        {
            this.urlEntryRepository = urlEntryRepository;
            this.urlIdGenerator = urlIdGenerator;
            this.urlIdStringConverter = urlIdStringConverter;
            this.urlEntryFactory = urlEntryFactory;
            this.cookieSessionIdProvider = cookieSessionIdProvider;
        }

        public async Task<UrlEntryHandlingResult> HandleAdd(UrlEntryRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Url))
                return new UrlEntryHandlingResult("Пустой запрос");

            if (!request.Url.StartsWith("http://") || !request.Url.StartsWith("https://"))
                return new UrlEntryHandlingResult("URL должен начинаться с http:// или https://");

            var urlId = urlIdGenerator.Get();

            var entry = urlEntryFactory.Create(
                urlId,
                request.Url,
                cookieSessionIdProvider.GetUserId()
            );

            await urlEntryRepository.AddUrlEntry(entry);

            return new UrlEntryHandlingResult(new UrlEntryItemModel(urlIdStringConverter.ConvertFromNumber(urlId), entry.Url, entry.VisitorsCount));
        }
    }
}