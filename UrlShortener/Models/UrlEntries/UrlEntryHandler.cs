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
        private readonly IShortUrlFormatter shortUrlFormatter;

        public UrlEntryHandler(IUrlEntryRepository urlEntryRepository,
            IUrlIdGenerator urlIdGenerator,
            IUrlIdStringConverter urlIdStringConverter,
            IUrlEntryFactory urlEntryFactory,
            ICookieSessionIdProvider cookieSessionIdProvider,
            IShortUrlFormatter shortUrlFormatter)
        {
            this.urlEntryRepository = urlEntryRepository;
            this.urlIdGenerator = urlIdGenerator;
            this.urlIdStringConverter = urlIdStringConverter;
            this.urlEntryFactory = urlEntryFactory;
            this.cookieSessionIdProvider = cookieSessionIdProvider;
            this.shortUrlFormatter = shortUrlFormatter;
        }

        public async Task<UrlEntryHandlingResult> HandleAdd(UrlEntryRequest request)
        {
            if (request == null)
                return new UrlEntryHandlingResult("������ ������");

            return await HandleAdd(request.Url);
        }

        public async Task<UrlEntryHandlingResult> HandleAdd(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return new UrlEntryHandlingResult("������ ������");

            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                return new UrlEntryHandlingResult("URL ������ ���������� � http:// ��� https://");

            var urlId = urlIdGenerator.Get();

            var entry = urlEntryFactory.Create(
                urlId,
                url,
                cookieSessionIdProvider.GetUserId()
            );

            await urlEntryRepository.AddUrlEntry(entry);

            var urlIdString = urlIdStringConverter.ConvertFromNumber(urlId);

            return new UrlEntryHandlingResult(
                new UrlEntryItemModel(
                    urlIdString,
                    entry.Url,
                    shortUrlFormatter.Format(urlIdString),
                    entry.VisitorsCount));
        }
    }
}