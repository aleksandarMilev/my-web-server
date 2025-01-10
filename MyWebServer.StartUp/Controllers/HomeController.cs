namespace MyWebServer.StartUp.Controllers
{
    using HTTP.RequestResponse.Response;
    using Mvc;

    public class HomeController : MvcController
    {
        public IHttpResponse Index() => this.View();
    }
}
