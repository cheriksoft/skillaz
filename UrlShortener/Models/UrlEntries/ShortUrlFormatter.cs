using System;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Http;

namespace UrlShortener.Models.UrlEntries
{
    public class ShortUrlFormatter : IShortUrlFormatter
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ShortUrlFormatter(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string Format(string urlId)
        {
            var requestUrl = httpContextAccessor.HttpContext.Request.GetUri();

            return
                $"{requestUrl.Scheme}://{requestUrl.Host}{GetPort(requestUrl)}/{urlId}";
        }

        private string GetPort(Uri requestUrl)
        {
            if (requestUrl.Scheme == "http" && requestUrl.Port == 80)
                return string.Empty;

            if (requestUrl.Scheme == "https" && requestUrl.Port == 443)
                return string.Empty;

            return ":" + requestUrl.Port;
        }
    }
}