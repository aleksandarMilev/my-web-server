namespace MyWebServer.HTTP
{
    using System;
    using System.Threading.Tasks;
    using RequestResponse.Request;
    using RequestResponse.Response;

    using static Common.Constants;

    /// <summary>
    /// Defines the interface for an HTTP server that can handle routing and start listening for incoming requests.
    /// </summary>
    public interface IHttpServer
    {
        /// <summary>
        /// Registers a new route and its associated action. If an action is already defined for this route, it will be replaced with the new one.
        /// </summary>
        /// <param name="path">The path of the route to be handled by the server.</param>
        /// <param name="action">A function that takes an <see cref="IHttpRequest"/> and returns an <see cref="IHttpResponse"/> for the route.</param>
        /// <returns>An instance of <see cref="IHttpServer"/> to allow for method chaining.</returns>
        IHttpServer AddRoute(string path, Func<IHttpRequest, IHttpResponse> action);

        /// <summary>
        /// Starts the HTTP server asynchronously on the specified port.
        /// </summary>
        /// <param name="port">The port number on which the server will listen.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous start operation.</returns>
        Task StartAsync(int port = DefaultPort);
    }
}
