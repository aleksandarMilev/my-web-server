namespace MyWebServer.HTTP.RequestResponse.Request
{
    using System.Collections.Generic;

    public interface IHttpRequest
    {
        RequestLine RequestLine { get; }

        ICollection<Header> Headers { get; }

        ICollection<Cookie> Cookies { get; }

        string? Body { get; }
    }
}