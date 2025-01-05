namespace MyWebServer.HTTP.RequestResponse.Request
{
    using System;

    public class RequestLine
    {
        private const char RequestLineElementsSepartor = ' ';

        public RequestLine(string requestLine)
        {
            var requestLineParts = requestLine.Split(RequestLineElementsSepartor);

            this.Method = StringToHttpMethod(requestLineParts[0]);
            this.Path = requestLineParts[1];
            this.Version = requestLineParts[2];
        }

        public string Path { get; private set; } = null!;

        public HttpMethod Method { get; private set; }

        public string Version { get; private set; } = null!;

        public override string ToString() => $"{this.Method} {this.Path} {this.Version}";

        private static HttpMethod StringToHttpMethod(string value, bool ignoreCase = true)
        {
            if (Enum.TryParse(value, ignoreCase, out HttpMethod result))
            {
                return result;
            }

            throw new ArgumentException($"The value '{value}' is not valid for enum type {typeof(HttpMethod).Name}.");
        }
    }
}
