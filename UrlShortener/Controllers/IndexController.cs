using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models.UrlEntries;

namespace UrlShortener.Controllers
{
    [Route("")]
    public class IndexController : Controller
    {
        private readonly IUrlEntryModelBuilder urlEntryModelBuilder;

        public IndexController(IUrlEntryModelBuilder urlEntryModelBuilder)
        {
            this.urlEntryModelBuilder = urlEntryModelBuilder;
        }

        public Task<IList<UrlEntryItemModel>> GetUserEntries()
        {
            return urlEntryModelBuilder.GetForCurrentUser();
        }
    }
}