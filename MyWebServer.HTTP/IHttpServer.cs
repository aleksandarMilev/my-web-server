namespace MyWebServer.HTTP
{
    using System;
    using System.Threading.Tasks;

    public interface IHttpServer
    {
        void AddRoute(string path, Func<IHttpRequest, IHttpResponse> action);

        Task StartAsync (int port);
    }
}
