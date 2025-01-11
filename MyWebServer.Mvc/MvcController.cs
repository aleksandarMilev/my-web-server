namespace MyWebServer.Mvc
{
    using System.Runtime.CompilerServices;
    using HTTP.RequestResponse.Response;

    using static Common.Constants;

    public abstract class MvcController
    {
        public IHttpResponse View([CallerMemberName] string? action = null)
        {
            var controller = this
                .GetType()
                .Name
                .Replace(Controller, string.Empty);

            var path = $"Views/{controller}/{action}.html";

            return new HttpResponse(File.ReadAllText(path));
        }
    }
}
