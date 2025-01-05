namespace MyWebServer.HTTP.RequestResponse.Request
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using static Common.Constants.Common;

    public class HttpRequest : IHttpRequest
    {
        private const string CookieElementsSeparator = "; ";

        public HttpRequest(string request) => this.ParseRequest(request);

        public RequestLine RequestLine { get; private set; } = null!;

        public ICollection<Header> Headers { get; } = new List<Header>();

        public ICollection<Cookie> Cookies { get; } = new List<Cookie>();

        public string? Body { get; private set; }

        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine(this.RequestLine.ToString());

            foreach (var header in this.Headers)
            {
                result.AppendLine(header.ToString());
            }

            if (this.Body is not null)
            {
               result.AppendLine(this.Body);
            }

            return result.ToString();
        }

        private void ParseRequest(string request)
        {
            var lines = request.Split(NewLine, StringSplitOptions.None);

            this.ParseRequestLine(lines[0]);
            this.ParseHeadersAndBody(lines.Skip(1).ToArray());
        }

        private void ParseRequestLine(string requestLine) => this.RequestLine = new(requestLine);

        private void ParseHeadersAndBody(string[] lines)
        {
            var bodyStringBuilder = new StringBuilder();
            var isInHeaders = true;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    isInHeaders = false;
                    continue;
                }

                if (isInHeaders)
                {
                    this.Headers.Add(new Header(line));
                }
                else
                {
                    bodyStringBuilder.AppendLine(line);
                }
            }

            this.Body = bodyStringBuilder.ToString();
            this.ParseCookieIfExists();
        }

        private void ParseCookieIfExists()
        {
            var cookie = this.Headers.FirstOrDefault(h => h.Key == nameof(Cookie));

            if (cookie is not null && cookie.Value is not null)
            {
                cookie
                    .Value
                    .Split(
                        CookieElementsSeparator,
                        StringSplitOptions.RemoveEmptyEntries)
                    .ToList()
                    .ForEach(c => this.Cookies.Add(new Cookie(c)));
            }
        }
    }
}
