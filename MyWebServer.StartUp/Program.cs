namespace MyWebServer.StartUp
{
    using System.Text;
    using HTTP.Builder;
    using HTTP.RequestResponse.Response;

    using static HTTP.Common.Constants;

    internal class Program
    {
        private static async Task Main() 
            => await HttpServerBuilder
                .Build()
                .AddRoute(Home)
                .AddRoute(About)
                .AddRoute(Contacts)
                .AddRoute(Login)
                .StartAsync();

        private static IHttpResponse Home()
            => new HttpResponse(
                ServerName,
                ContentTypes.Html,
                Encoding.UTF8.GetBytes("<h1>Home!</h1>"));

        private static IHttpResponse About()
            => new HttpResponse(
                ServerName,
                ContentTypes.Html,
                Encoding.UTF8.GetBytes("<h1>About!</h1>"));

        private static IHttpResponse Contacts()
            => new HttpResponse(
                ServerName,
                ContentTypes.Html,
                Encoding.UTF8.GetBytes("<h1>Contacts!</h1>"));

        private static IHttpResponse Login()
            => new HttpResponse(
                ServerName,
                ContentTypes.Html,
                Encoding.UTF8.GetBytes("<h1>Login!</h1>"));
    }
}
