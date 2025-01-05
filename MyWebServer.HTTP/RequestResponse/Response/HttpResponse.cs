namespace MyWebServer.HTTP.RequestResponse.Response
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using static Common.Constants;
    using static Constants;

    public class HttpResponse : IHttpResponse
    {
        private readonly ICollection<Header> headers = new List<Header>();
        private readonly ICollection<ResponseCookie> cookies = new List<ResponseCookie>();

        public HttpResponse(
            string serverName,
            string contentType,
            byte[] body,
            HttpStatus status = HttpStatus.Ok)
        {
            this.headers = new List<Header>()
            {
               new(HeaderKeys.Server, serverName),
               new(HeaderKeys.ContentType, contentType),
               new(HeaderKeys.ContentLength, body.Length.ToString())
            };

            this.Body = body;
            this.StatusLine = new(Version.Three, status);

            this.cookies = new List<ResponseCookie>();
        }

        public StatusLine StatusLine { get; }

        public IReadOnlyCollection<Header> Headers => this.headers.ToList().AsReadOnly();

        public IReadOnlyCollection<ResponseCookie> Cookies => this.cookies.ToList().AsReadOnly();

        public byte[] Body { get; }

        public override string ToString()
        {
            var response = new StringBuilder();

            response.Append(this.StatusLine.ToString() + NewLine);

            foreach (var header in this.headers)
            {
                response.Append(header.ToString() + NewLine);
            }

            foreach (var cookie in this.cookies)
            {
                response.Append(HeaderKeys.SetCookie + cookie.ToString() + NewLine);
            }

            response.Append(NewLine);
            response.Append(Encoding.UTF8.GetString(this.Body));

            return response.ToString().TrimEnd();
        }

        public byte[] ToByteArray() => Encoding.UTF8.GetBytes(this.ToString());
    }
}
