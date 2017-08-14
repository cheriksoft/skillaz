using System;

namespace UrlShortener.Infrastructure
{
    public interface ICookieSessionIdProvider
    {
        Guid GetUserId();
    }
}