using System.Threading.Tasks;

namespace UrlShortener.Models.UrlEntries
{
    public interface IUrlEntryHandler
    {
        Task<UrlEntryHandlingResult> HandleAdd(UrlEntryRequest request);
    }
}