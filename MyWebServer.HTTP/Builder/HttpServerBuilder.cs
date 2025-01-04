namespace MyWebServer.HTTP.Builder
{
    /// <summary>
    /// Provides a static builder for constructing an <see cref="IHttpServer"/> instance.
    /// </summary>
    public static class HttpServerBuilder
    {
        /// <summary>
        /// Builds and returns an instance of <see cref="IHttpServer"/>.
        /// </summary>
        /// <returns>An instance of <see cref="IHttpServer"/> that is ready to be used.</returns>
        public static IHttpServer Build() => new HttpServer();
    }
}
