namespace MyWebServer.HTTP.RequestResponse
{
    using System;

    public class Header
    {
        private const int SeparatedElementsCount = 2;
        private const string Separator = ": ";

        public Header(string header)
        {
            var headerParts = header.Split(
                Separator,
                SeparatedElementsCount,
                StringSplitOptions.None);

            this.Key = headerParts[0];
            this.Value = headerParts[1];
        }

        public string Key { get; } = null!;

        public string? Value { get; }

        public override string ToString() => $"{this.Key}: {this.Value}";
    }
}
