namespace MyWebServer.HTTP
{
    using System;

    public class HttpServer : IHttpServer
    {
        public void AddRoute(string path, Func<IHttpRequest, IHttpResponse> action) => throw new System.NotImplementedException();

        public void Start(int port) => throw new System.NotImplementedException();
    }
}
