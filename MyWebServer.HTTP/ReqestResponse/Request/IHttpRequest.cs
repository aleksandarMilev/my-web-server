namespace MyWebServer.HTTP.ReqestResponse.Request
{
    using System.Collections.Generic;

    public interface IHttpRequest
    {
        string Path { get; }

        HttpMethod Method { get; }

        ICollection<Header> Headers { get; }

        ICollection<Cookie> Cookies { get; }

        string? Body { get; }
    }
}