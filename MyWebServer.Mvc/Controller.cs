namespace MyWebServer.Mvc
{
    using HTTP.RequestResponse.Response;

    public abstract class Controller
    {
        public IHttpResponse View(string path) => new HttpResponse(File.ReadAllText(path));
    }
}
