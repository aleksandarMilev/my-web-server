namespace MyWebServer.HTTP.RequestResponse
{
    public class Cookie
    {
        private const char KeyValueSeparator = '=';
        private const int KeyValuesCount = 2;

        public Cookie(string cookie)
        {
            var keyValuePair = cookie.Split(KeyValueSeparator, KeyValuesCount);

            this.Key = keyValuePair[0];
            this.Value = keyValuePair[1];
        }

        public string Key { get; }

        public string Value { get; }

        public override string ToString() => $"{this.Key}={this.Value}";
    }
}
