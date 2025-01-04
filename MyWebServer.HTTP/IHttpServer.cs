namespace MyWebServer.HTTP
{
    using System;

    public interface IHttpServer
    {
        void AddRoute(string path, Func<IHttpRequest, IHttpResponse> action);

        void Start(int port);
    }
}
