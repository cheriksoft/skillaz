namespace UrlShortener.Infrastructure
{
    public interface IUrlIdStringConverter
    {
        string ConvertFromNumber(long number);
        long ConvertFromString(string urlId);
    }
}