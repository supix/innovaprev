using System.Net;
using DomainModel.Services;

namespace ProductImages
{
    internal class ImageProvider_FromUrl : IImageProvider
    {
        private readonly string baseUrl;

        public ImageProvider_FromUrl(string baseUrl)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new ArgumentException($"'{nameof(baseUrl)}' non può essere Null o uno spazio vuoto.", nameof(baseUrl));
            }

            this.baseUrl = baseUrl;
        }

        public byte[] Get(string name)
        {
            var baseUri = new Uri(this.baseUrl);
            var fullUri = new Uri(baseUri, name);
            using (var client = new HttpClient())
            {
                byte[] imageBytes = client.GetByteArrayAsync(fullUri).Result;
                return imageBytes;
            }
        }
    }
}
