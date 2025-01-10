namespace MyWebServer.HTTP
{
    using System;
    using System.Threading.Tasks;
    using RequestResponse.Response;

    using static Common.Constants;

   
    public interface IHttpServer
    {
        IHttpServer AddRoute(Func<IHttpResponse> action, string path);

        Task StartAsync(int port = DefaultPort);
    }
}
