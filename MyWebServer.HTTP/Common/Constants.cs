namespace MyWebServer.HTTP.Common
{
    public static class Constants
    {
        public class Common
        {
            public const int Zero = 0;

            public const string NewLine = "\r\n";
        }

        public class HttpServer
        {
            public const int BufferInitLength = 4_096;

            public const int DefaultPort = 80;
        }
    }
}
