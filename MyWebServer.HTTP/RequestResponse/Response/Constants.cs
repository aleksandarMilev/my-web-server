namespace MyWebServer.HTTP.RequestResponse.Response
{
    public static class Constants
    {
        public class Version
        {
            public const string Three = "HTTP/3";
        }

        public class HeaderKeys
        {
            public const string Server = "Server";
            public const string ContentType = "Content-Type";
            public const string ContentLength = "Content-Length";
            public const string SetCookie = "Set-Cookie";
        }
    }
}
