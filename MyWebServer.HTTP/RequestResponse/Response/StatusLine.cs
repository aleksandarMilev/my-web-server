namespace MyWebServer.HTTP.RequestResponse.Response
{
    public class StatusLine
    {
        public StatusLine(string version, HttpStatus status)
        {
            this.Version = version;
            this.Status = status;
        }

        public string Version { get; }

        public HttpStatus Status { get; }

        public override string ToString() => $"{this.Version} {(int)this.Status} {this.Status}";
    }
}
