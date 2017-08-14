using System;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace UrlShortener.Infrastructure
{
    public class CookieSessionIdProvider : ICookieSessionIdProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public const string COOKIE_KEY = "sessid";

        public CookieSessionIdProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserId()
        {
            Guid cookieId;

            var cookie = httpContextAccessor.HttpContext.Request.Cookies[COOKIE_KEY];

            if (cookie != null)
            {
                cookieId = Guid.Parse(cookie);
            }
            else
            {
                cookieId = Guid.NewGuid();
                httpContextAccessor.HttpContext.Response.Cookies.Append(COOKIE_KEY, cookieId.ToString(), new CookieOptions()
                {
                    Expires = DateTimeOffset.MaxValue
                });
            }

            return cookieId;
        }
    }
}