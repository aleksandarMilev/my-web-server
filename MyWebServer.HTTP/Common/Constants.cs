﻿namespace MyWebServer.HTTP.Common
{
    public static class Constants
    {
        public const int DefaultPort = 80;

        public const string NewLine = "\r\n";

        public const string RouteSeparator = "/";

        public const string ServerName = "My Web Server";
        
        public const string IndexActionRoute = "index";

        public const string HomeControllerRoute = "home";

        public class ContentTypes
        {
            public const string Html = "text/html";
        }
    }
}
