namespace MyWebServer.HTTP
{
    using System;
    using System.Collections.Generic;

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

        public void Start(int port) => throw new System.NotImplementedException();
    }
}
