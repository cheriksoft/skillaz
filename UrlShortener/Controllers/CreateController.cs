using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models.UrlEntries;

namespace UrlShortener.Controllers
{
    [Route("create")]
    public class CreateController : Controller
    {
        private readonly IUrlEntryHandler urlEntryHandler;

        public CreateController(IUrlEntryHandler urlEntryHandler)
        {
            this.urlEntryHandler = urlEntryHandler;
        }

        [HttpPost]
        public async Task<UrlEntryHandlingResult> Create([FromBody] UrlEntryRequest request)
        {
            var result = await urlEntryHandler.HandleAdd(request);

            if (!result.Success)
                Response.StatusCode = 400;

            return result;
        }
    }
}