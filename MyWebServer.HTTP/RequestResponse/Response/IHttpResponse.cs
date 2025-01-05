namespace MyWebServer.HTTP.RequestResponse.Response
{
    using System.Collections.Generic;

    public interface IHttpResponse
    {
        StatusLine StatusLine { get; }

        IReadOnlyCollection<Header> Headers { get; }

        IReadOnlyCollection<ResponseCookie> Cookies { get; }

        byte[] Body { get; }

        byte[] ToByteArray();
    }
}
