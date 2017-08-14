namespace UrlShortener.Repositories
{
    public interface IUrlEntryRepository
    {
        long GetMaxUrlId();
    }
}