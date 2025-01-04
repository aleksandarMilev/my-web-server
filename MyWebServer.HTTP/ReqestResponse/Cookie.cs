namespace MyWebServer.HTTP.ReqestResponse
{
    public readonly struct Cookie
    {
        public Cookie(string key, string? value)
        {
            this.Key = key;
            this.Value = value;
        }

        public string Key { get; } = null!;

        public string? Value { get; }
    }
}
