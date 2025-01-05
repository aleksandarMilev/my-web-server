namespace MyWebServer.HTTP
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;
    using RequestResponse.Request;
    using RequestResponse.Response;

    using static Common.Constants;

    /// <summary>
    /// Represents an HTTP server that handles routing and processes incoming requests.
    /// </summary>
    public class HttpServer : IHttpServer
    {
        private const int BufferInitLength = 4_096;
        private const string ServerName = "My Web Server";

        private readonly IDictionary<string, Func<IHttpRequest, IHttpResponse>>
            routeTable = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>();

        internal HttpServer() { } //Only IHttpServerBuilder should create new instances of this class in other assemblies

        /// <inheritdoc />
        public IHttpServer AddRoute(string path, Func<IHttpRequest, IHttpResponse> action)
        {
            if (!this.routeTable.TryAdd(path, action))
            {
                this.routeTable[path] = action;
            }

            return this;
        }

        /// <inheritdoc />
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

        private static async Task ProcessClientAsync(TcpClient tcpClient)
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

            var responseBodyBytes = Encoding.UTF8.GetBytes("<h1>Hello, World!</h1>");
            var response = new HttpResponse(
                ServerName,
                ContentTypes.Html,
                responseBodyBytes);

            await stream.WriteAsync(response.ToByteArray());
        }
    }
}
