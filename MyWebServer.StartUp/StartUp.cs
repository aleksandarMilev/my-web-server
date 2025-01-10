namespace MyWebServer.StartUp
{
    using Mvc;

    internal class Program
    {
        private static async Task Main()
            => await new WebApplicationBuilder()
                .MapControllers()
                .StartAsync();
    }
}
