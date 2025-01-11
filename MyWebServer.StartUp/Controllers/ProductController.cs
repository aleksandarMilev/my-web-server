namespace MyWebServer.StartUp.Controllers
{
    using HTTP.RequestResponse.Response;
    using Mvc;

    public class ProductController : MvcController
    {
        public IHttpResponse Mine() => this.View();
    }
}
