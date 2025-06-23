using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using DomainModel.Classes;
using DomainModel.Services.WindowImageRenderer;

namespace WindowImageRenderer_WebService
{
    class WindowImageRenderer : IWindowImageRenderer
    {
        private readonly string baseUri;
        private string[]? allowedMaterials = null;
        public WindowImageRenderer(string baseUri)
        {
            if (string.IsNullOrWhiteSpace(baseUri))
            {
                throw new ArgumentException($"'{nameof(baseUri)}' non può essere Null o uno spazio vuoto.", nameof(baseUri));
            }

            this.baseUri = baseUri;
        }

        public bool CanRender(string materialType)
        {
            this.loadAllowedMaterials();
            return this.allowedMaterials!.Contains(materialType);
        }

        public byte[] Render(long height_mm, long width_mm, string materialType, bool wireCover, string openingType, string glassType)
        {
            const string apiPath = "/drawWindow";

            NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString.Add("height", height_mm.ToString());
            queryString.Add("width", width_mm.ToString());
            queryString.Add("materialType", materialType.ToString());
            queryString.Add("wireCover", wireCover ? "true" : "false");
            queryString.Add("openingType", openingType);
            queryString.Add("glassType", glassType);

            Uri uri = new Uri($"{baseUri}{apiPath}?{queryString.ToString()}");

            using var client = new HttpClient();
            byte[] imageBytes = client.GetByteArrayAsync(uri).Result;
            return imageBytes;
        }

        private void loadAllowedMaterials()
        {
            const string apiPath = "/drawableWindow";

            if (this.allowedMaterials != null)
                return;

            Uri uri = new Uri($"{baseUri}{apiPath}");

            using var client = new HttpClient();
            var response = client.GetAsync(uri).Result;
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;

            using var doc = JsonDocument.Parse(content);
            var root = doc.RootElement;
            var dataArray = root.GetProperty("data");

            string[] values = new string[dataArray.GetArrayLength()];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = dataArray[i].GetString()!;
            }

            this.allowedMaterials = values;
        }
    }
}
