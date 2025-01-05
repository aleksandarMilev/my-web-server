namespace MyWebServer.StartUp
{
    using HTTP.Builder;
    using HTTP.RequestResponse.Response;

    internal class Program
    {
        private static async Task Main() 
            => await HttpServerBuilder
                .Build()
                .AddRoute(() => new HttpResponse("<h1>Home</h1>"), "/home")
                .AddRoute(() => new HttpResponse("<h1>Login</h1>"), "/login")
                .AddRoute(() => new HttpResponse("<h1>About</h1>"), "/about")
                .AddRoute(() => new HttpResponse("<h1>Contacts</h1>"), "/contacts")
                .StartAsync();
    }
}
