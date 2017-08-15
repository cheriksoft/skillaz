using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models.UrlEntries;

namespace UrlShortener.Controllers
{
    [Route("")]
    public class EntryController : Controller
    {
        private readonly IUrlEntryModelBuilder urlEntryModelBuilder;
        private readonly IUrlEntryHandler urlEntryHandler;

        public EntryController(IUrlEntryModelBuilder urlEntryModelBuilder,
            IUrlEntryHandler urlEntryHandler)
        {
            this.urlEntryModelBuilder = urlEntryModelBuilder;
            this.urlEntryHandler = urlEntryHandler;
        }

        [HttpGet("mylinks")]
        public Task<IList<UrlEntryItemModel>> GetUserEntries()
        {
            return urlEntryModelBuilder.GetForCurrentUser();
        }

        [HttpGet("create")]
        public async Task<UrlEntryHandlingResult> Create(string url)
        {
            var result = await urlEntryHandler.HandleAdd(url);

            if (!result.Success)
                Response.StatusCode = 400;

            return result;
        }

        [HttpPost("create")]
        public async Task<UrlEntryHandlingResult> Create([FromBody]UrlEntryRequest request)
        {
            var result = await urlEntryHandler.HandleAdd(request);

            if (!result.Success)
                Response.StatusCode = 400;

            return result;
        }

        [HttpGet("{urlId}")]
        public void Get(string urlId)
        {
            var entry = urlEntryModelBuilder.GetByStringUrlId(urlId);

            Response.StatusCode = 302;
            Response.Headers["Location"] = entry.Result.LongUrl;
        }
        
        

        
    }
}