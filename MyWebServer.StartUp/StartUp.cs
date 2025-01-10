namespace MyWebServer.StartUp
{
    using Controllers;
    using HTTP.Builder;

    internal class Program
    {
        private static async Task Main()
            => await HttpServerBuilder
                .Build()
                .AddRoute(new HomeController().Index)
                .StartAsync();
    }
}
