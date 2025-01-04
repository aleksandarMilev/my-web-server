namespace MyWebServer.HTTP
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;

    public class HttpServer : IHttpServer
    {
        private readonly Dictionary<string, Func<IHttpRequest, IHttpResponse>> routeTable = [];

        public void AddRoute(string path, Func<IHttpRequest, IHttpResponse> action)
        {
            if (this.routeTable.ContainsKey(path))
            {
                this.routeTable[path] = action;
                return;
            }

            this.routeTable.Add(path, action);
        }

        public async Task StartAsync(int port)
        {
            using var tcpListener = new TcpListener(IPAddress.Loopback, port);
            tcpListener.Start();

            while (true)
            {
                var tcpClient = await tcpListener.AcceptTcpClientAsync();

#pragma warning disable CS4014
                //we're not awaiting this because we don't need the result from processing the current client.
                ProcessClientAsync(tcpClient);
#pragma warning restore CS4014

            }
        }

        private async Task ProcessClientAsync(TcpClient tcpClient)
        {
        }
    }
}
