namespace MyWebServer.HTTP.RequestResponse.Request
{
    using System.Collections.Generic;

    public interface IHttpRequest
    {
        RequestLine RequestLine { get; }

        IReadOnlyCollection<Header> Headers { get; }

        IReadOnlyCollection<RequestCookie> Cookies { get; }

        string? Body { get; }
    }
}