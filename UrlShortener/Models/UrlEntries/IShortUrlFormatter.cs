namespace UrlShortener.Models.UrlEntries
{
    public interface IShortUrlFormatter
    {
        string Format(string urlId);
    }
}