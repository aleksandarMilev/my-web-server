namespace MyWebServer.HTTP.RequestResponse.Request
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using static Common.Constants;

    public class HttpRequest : IHttpRequest
    {
        private const string CookieElementsSeparator = "; ";

        private readonly ICollection<Header> headers = new List<Header>();
        private readonly ICollection<RequestCookie> cookies = new List<RequestCookie>();

        public HttpRequest(string request) => this.ParseRequest(request);

        public RequestLine RequestLine { get; private set; } = null!;

        public IReadOnlyCollection<Header> Headers => this.headers.ToList().AsReadOnly();

        public IReadOnlyCollection<RequestCookie> Cookies => this.cookies.ToList().AsReadOnly();

        public string? Body { get; private set; }

        public override string ToString()
        {
            var request = new StringBuilder();

            request.AppendLine(this.RequestLine.ToString());

            foreach (var header in this.headers)
            {
                request.AppendLine(header.ToString());
            }

            if (this.Body is not null)
            {
                request.AppendLine(this.Body);
            }

            return request.ToString().TrimEnd();
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
                    this.headers.Add(new Header(line));
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
            var cookie = this.headers.FirstOrDefault(h => h.Key == nameof(Cookie));

            if (cookie is not null && cookie.Value is not null)
            {
                cookie
                    .Value
                    .Split(
                        CookieElementsSeparator,
                        StringSplitOptions.RemoveEmptyEntries)
                    .ToList()
                    .ForEach(c => this.cookies.Add(new RequestCookie(c)));
            }
        }
    }
}
