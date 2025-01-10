namespace MyWebServer.HTTP
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using RequestResponse.Request;
    using RequestResponse.Response;

    using static Common.Constants;

    public class HttpServer : IHttpServer
    {
        private const int BufferInitLength = 4_096;

        private readonly Dictionary<string, Func<IHttpResponse>> routes = [];

        public IHttpServer AddRoute(Func<IHttpResponse> action, string path)
        {
            this.routes[path] = action;
            return this;
        }

        public async Task StartAsync(int port = DefaultPort)
        {
            using var tcpListener = new TcpListener(IPAddress.Loopback, port);
            tcpListener.Start();

            while (true)
            {
                using var tcpClient = await tcpListener.AcceptTcpClientAsync();
                await ProcessClientAsync(tcpClient);
            }
        }

        private async Task ProcessClientAsync(TcpClient tcpClient)
        {
            using var stream = tcpClient.GetStream();

            var position = 0;
            var buffer = new byte[BufferInitLength];
            var data = new List<byte>();

            while (true)
            {
                var count = await stream.ReadAsync(buffer.AsMemory(position, buffer.Length));
                position += count;

                if (count < buffer.Length)
                {
                    var partialBuffer = new byte[count];
                    Array.Copy(buffer, partialBuffer, count);
                    data.AddRange(partialBuffer);

                    break;
                }
                else
                {
                    data.AddRange(buffer);
                }
            }

            var requestString = Encoding.UTF8.GetString(data.ToArray());
            var request = new HttpRequest(requestString);

            IHttpResponse response;

            if (this.routes.TryGetValue(request.RequestLine.Path.ToLower(), out var action))
            {
                response = action();
            }
            else
            {
                response = new HttpResponse("<h1>Ooops... the page you are looking for was not found!</h1>");
            }

            await stream.WriteAsync(response.ToByteArray());
        }
    }
}
