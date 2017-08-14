using System.Threading;
using UrlShortener.Repositories;

namespace UrlShortener.Infrastructure
{
    public class UrlIdGenerator : IUrlIdGenerator
    {
        private readonly IUrlEntryRepository urlEntryRepository;

        private long urlIdCounter;

        public UrlIdGenerator(IUrlEntryRepository urlEntryRepository)
        {
            this.urlEntryRepository = urlEntryRepository;
            urlIdCounter = urlEntryRepository.GetMaxUrlId();
        }

        public long Get()
        {
            return Interlocked.Increment(ref urlIdCounter);
        }
    }
}