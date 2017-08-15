namespace UrlShortener.Models.UrlEntries
{
    public class UrlEntryItemModel
    {
        public string UrlId { get; set; }

        public string LongUrl { get; set; }

        public string ShortUrl { get; set; }

        public long VisitorsCount { get; set; }

        public UrlEntryItemModel(string urlId, string longUrl, string shortUrl, long visitorsCount)
        {
            UrlId = urlId;
            LongUrl = longUrl;
            ShortUrl = shortUrl;
            VisitorsCount = visitorsCount;
        }
    }
}