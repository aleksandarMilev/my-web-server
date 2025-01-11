namespace MyWebServer.Mvc
{
    using System.Reflection;
    using HTTP;
    using HTTP.RequestResponse.Response;

    using static Common.Constants;

    public class WebApplicationBuilder
    {
        private readonly HttpServer server = new();

        public async Task RunAsync() => await this.server.StartAsync();

        public IHttpServer MapControllers()
        {
            var controllerTypes = Assembly
                .GetCallingAssembly()
                .GetTypes()
                .Where(t => 
                    t.IsClass && 
                    !t.IsAbstract && 
                    t.IsSubclassOf(typeof(MvcController)
                ));

            foreach (var controllerType in controllerTypes)
            {
                var controllerInstance = Activator.CreateInstance(controllerType);

                var actions = controllerType
                    .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .Where(m => m.ReturnType == typeof(IHttpResponse));

                foreach (var action in actions)
                {
                    if (action.Name == View)
                    {
                        continue;
                    }

                    var controller = controllerType.Name.Replace("Controller", "").ToLower();

                    var path = $"{RouteSeparator}{controller}{RouteSeparator}{action.Name.ToLower()}";

                    var actionDelegate = () => (IHttpResponse)action.Invoke(controllerInstance, null)!;

                    this.server.AddRoute(actionDelegate, path);
                }
            }

            return this.server;
        }
    }
}
