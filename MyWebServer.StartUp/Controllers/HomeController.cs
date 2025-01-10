namespace MyWebServer.StartUp.Controllers
{
    using MyWebServer.HTTP.RequestResponse.Response;
    using MyWebServer.Mvc;

    public class HomeController : Controller
    {
        public IHttpResponse Index() => this.View("Views/Home/Index.html");
    }
}
