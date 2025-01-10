namespace MyWebServer.Mvc
{
    using System.Runtime.CompilerServices;
    using HTTP.RequestResponse.Response;

    using static Common.Constants;

    public abstract class MvcController
    {
        public IHttpResponse View(
            [CallerFilePath] string filePath = "",
            [CallerMemberName] string action = "")
        {
            var controllerClassName = Path.GetFileNameWithoutExtension(filePath);
            var controller = controllerClassName?.Remove(
                controllerClassName.IndexOf(Controller),
                Controller.Length);

            var path = $"Views/{controller}/{action}.html";

            return new HttpResponse(File.ReadAllText(path));
        }
    }
}
