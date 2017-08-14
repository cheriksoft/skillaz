using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models.UrlEntries;

namespace UrlShortener.Controllers
{
    [Route("")]
    public class EntryController : Controller
    {
        private readonly IUrlEntryModelBuilder urlEntryModelBuilder;

        public EntryController(IUrlEntryModelBuilder urlEntryModelBuilder)
        {
            this.urlEntryModelBuilder = urlEntryModelBuilder;
        }

        [HttpGet("{urlId}")]
        public Task<UrlEntryItemModel> Get(string urlId)
        {
            var entry = urlEntryModelBuilder.GetByStringUrlId(urlId);

            return entry;
        }
    }
}