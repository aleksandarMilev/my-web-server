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

        private readonly IDictionary<string, Func<IHttpResponse>> routeTable = new Dictionary<string, Func<IHttpResponse>>();

        internal HttpServer() { } //Only IHttpServerBuilder should create new instances of this class in other assemblies

        /// <inheritdoc />
        public IHttpServer AddRoute(Func<IHttpResponse> action, string? path = null)
        {
            path ??= $"{RouteSeparator}{action.Method.Name.ToLower()}";

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

            if (this.routeTable.TryGetValue(request.RequestLine.Path.ToLower(), out var action))
            {
                response = action();
            }
            else
            {
                response = new HttpResponse(
                    ServerName,
                    ContentTypes.Html,
                    Encoding.UTF8.GetBytes("<h1>Ooops... the page you are looking for was not found!</h1>"),
                    HttpStatus.NotFound);
            }

            await stream.WriteAsync(response.ToByteArray());
        }
    }
}
