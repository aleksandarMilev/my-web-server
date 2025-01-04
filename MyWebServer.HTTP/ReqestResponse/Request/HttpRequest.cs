namespace MyWebServer.HTTP.ReqestResponse.Request
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using static Common.Constants.Common;

    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string request)
        {
            var lines = request.Split(NewLine, StringSplitOptions.None);

            var headerLine = lines[0];
            var headerLineParts = headerLine.Split(' ');

            this.Method = StringToHttpMethod(headerLineParts[0]);
            this.Path = headerLineParts[1];

            var lineIndex = 1;
            var isInHeaders = true;
            var bodyStringBuilder = new StringBuilder();

            while (lineIndex < lines.Length)
            {
                var currentLine = lines[lineIndex];
                lineIndex++;

                if (string.IsNullOrWhiteSpace(currentLine))
                {
                    isInHeaders = false;
                    continue;
                }

                if (isInHeaders)
                {
                    this.Headers.Add(new Header(currentLine));
                }
                else
                {
                    bodyStringBuilder.AppendLine(currentLine);
                }
            }

            this.Body = bodyStringBuilder.ToString();
        }

        public string Path { get; }

        public HttpMethod Method { get; }

        public string? Body { get; }

        public ICollection<Header> Headers { get; } = new List<Header>();

        public ICollection<Cookie> Cookies { get; } = new List<Cookie>();

        public override string ToString()
        {
            var result = new StringBuilder();
            result.AppendLine($"Path: {this.Path}");

            foreach (var header in this.Headers)
            {
                result.AppendLine(header.ToString());
            }

            result.AppendLine(this.Body);

            return result.ToString();
        }

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
