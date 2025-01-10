namespace MyWebServer.StartUp.Controllers
{
    using Mvc;
    using HTTP.RequestResponse.Response;

    public class ProductController : MvcController
    {
        public IHttpResponse Mine() => this.View();
    }
}
