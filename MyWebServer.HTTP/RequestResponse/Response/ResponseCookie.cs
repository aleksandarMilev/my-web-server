namespace MyWebServer.HTTP.RequestResponse.Response
{
    using System.Text;

    public class ResponseCookie : Cookie
    {
        private const string Secure = "Secure";
        private const string HttpOnly = "HttpOnly";
        private const string Separator = "; ";

        public ResponseCookie(string cookie)
            : base(cookie) { }

        public string? Path { get; set; }

        public string? Domain { get; set; }

        public string? MaxAge { get; set; }

        public string? Expires { get; set; }

        public string? SameSite { get; set; }

        public bool IsSecure { get; set; }

        public bool IsHttpOnly { get; set; }


        public override string ToString()
        {
            var cookie = new StringBuilder();

            cookie.AppendLine(base.ToString());

            if (!string.IsNullOrWhiteSpace(this.Path))
            {
                cookie.Append($"{Separator}{nameof(this.Path)}={this.Path}");
            }

            if (!string.IsNullOrWhiteSpace(this.Domain))
            {
                cookie.Append($"{Separator}{nameof(this.Domain)}={this.Domain}");
            }

            if (!string.IsNullOrWhiteSpace(this.MaxAge))
            {
                cookie.Append($"{Separator}{nameof(this.MaxAge)}={this.MaxAge}");
            }

            if (!string.IsNullOrWhiteSpace(this.Expires))
            {
                cookie.Append($"{Separator}{nameof(this.Expires)}={this.Expires}");
            }

            if (!string.IsNullOrWhiteSpace(this.SameSite))
            {
                cookie.Append($"{Separator}{nameof(this.SameSite)}={this.SameSite}");
            }

            if (this.IsSecure)
            {
                cookie.Append($"{Separator}{Secure}");
            }

            if (this.IsHttpOnly)
            {
                cookie.Append($"{Separator}{HttpOnly}");
            }

            return cookie.ToString().TrimEnd();
        }
    }
}
