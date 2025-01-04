namespace MyWebServer.HTTP
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;
    using ReqestResponse.Request;
    using ReqestResponse.Response;

    using static Common.Constants.Common;
    using static Common.Constants.HttpServer;

    /// <summary>
    /// Represents an HTTP server that handles routing and processes incoming requests.
    /// </summary>
    public class HttpServer : IHttpServer
    {
        private readonly IDictionary<string, Func<IHttpRequest, IHttpResponse>> 
            routeTable = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>();

        internal HttpServer() { } //Only IHttpServerBuilder should create new instances of this class in other assembly

        /// <inheritdoc />
        public IHttpServer AddRoute(string path, Func<IHttpRequest, IHttpResponse> action)
        {
            if (this.routeTable.ContainsKey(path))
            {
                this.routeTable[path] = action;
            }
            else
            {
                this.routeTable.Add(path, action);
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

#pragma warning disable CS4014
                //we're not awaiting this because we don't need the result from processing the current client.
                ProcessClientAsync(tcpClient);
#pragma warning restore CS4014

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

            var resquestString = Encoding.UTF8.GetString(data.ToArray());
            var httpRequest = new HttpRequest(resquestString);
            Console.WriteLine(httpRequest);

            var responseBody = "<h1>Hello, World!</h1>";
            var responseBodyAsByteArray = Encoding.UTF8.GetBytes(responseBody);

            var response =
                "HTTP/3 200 OK" + NewLine + 
                "Server: MyWebServer" + NewLine + 
                "Content-Type: text/html" + NewLine + 
                $"Content-Lenght: {responseBodyAsByteArray.Length}" + NewLine + NewLine;

            var reponseAsByteArray = Encoding.UTF8.GetBytes(response);

            await stream.WriteAsync(reponseAsByteArray);
            await stream.WriteAsync(responseBodyAsByteArray);
        }
    }
}
