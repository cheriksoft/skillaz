namespace UrlShortener.Models.UrlEntries
{
    public class UrlEntryItemModel
    {
        public string UrlId { get; set; }

        public string Url { get; set; }

        public long VisitorsCount { get; set; }

        public UrlEntryItemModel(string urlId, string url, long visitorsCount)
        {
            UrlId = urlId;
            Url = url;
            VisitorsCount = visitorsCount;
        }
    }
}