﻿namespace MyWebServer.HTTP.RequestResponse.Request
{
    using System;

    using static Common.Constants;

    public class RequestLine
    {
        private const char RequestLineElementsSeparator = ' ';

        public RequestLine(string requestLine)
        {
            var requestLineParts = requestLine.Split(RequestLineElementsSeparator);

            this.Method = StringToHttpMethod(requestLineParts[0]);

            if (requestLineParts[1] == RouteSeparator ||
                requestLineParts[1] == $"{RouteSeparator}{HomeControllerRoute}" ||
                requestLineParts[1] == string.Empty)
            {
                this.Path = $"{RouteSeparator}{HomeControllerRoute}{RouteSeparator}{IndexActionRoute}";
            }
            else
            {
                this.Path = requestLineParts[1];
            }

            this.Version = requestLineParts[2];
        }

        public string Path { get; } = null!;

        public HttpMethod Method { get; }

        public string Version { get; } = null!;

        public override string ToString() => $"{this.Method} {this.Path} {this.Version}";

        private static HttpMethod StringToHttpMethod(string value, bool ignoreCase = true)
        {
            if (Enum.TryParse(value, ignoreCase, out HttpMethod result))
            {
                return result;
            }

            return HttpMethod.Get;
        }
    }
}
