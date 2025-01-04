namespace MyWebServer.StartUp
{
    using HTTP.Builder;

    internal class Program
    {
        private static async Task Main()
        {
            await HttpServerBuilder
                .Build()
                .StartAsync();
        }
    }
}
