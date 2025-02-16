using DomainModel.Services;

namespace ProductImages
{
    internal class FromUrl : IProductImageProvider
    {
        private readonly string baseUrl;

        public FromUrl(string baseUrl)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new ArgumentException($"'{nameof(baseUrl)}' non può essere Null o uno spazio vuoto.", nameof(baseUrl));
            }

            this.baseUrl = baseUrl;
        }

        public byte[] Get(string name, bool isThumb)
        {
            var baseUri = new Uri(baseUrl);
            var fullUri = new Uri(baseUri, name);
            using (var client = new HttpClient())
            {
                byte[] imageBytes = client.GetByteArrayAsync(fullUri).Result;
                return imageBytes;
            }
        }
    }
}
