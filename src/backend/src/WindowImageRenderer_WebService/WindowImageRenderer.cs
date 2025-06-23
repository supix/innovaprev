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
        private readonly string serverUri;
        private string[]? allowedMaterials = null;
        public WindowImageRenderer(string serverUri)
        {
            if (string.IsNullOrWhiteSpace(serverUri))
            {
                throw new ArgumentException($"'{nameof(serverUri)}' non può essere Null o uno spazio vuoto.", nameof(serverUri));
            }

            this.serverUri = serverUri;
        }

        public bool CanRender(string materialType)
        {
            this.loadAllowedMaterials();
            return this.allowedMaterials!.Contains(materialType);
        }

        public byte[] Render(long height_mm, long width_mm, string materialType, bool wireCover, string openingType, string glassType)
        {
            const string apiPath = "/api/windows/drawWindow";

            NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString.Add("height", height_mm.ToString());
            queryString.Add("width", width_mm.ToString());
            queryString.Add("materialType", materialType.ToString());
            queryString.Add("wireCover", wireCover ? "true" : "false");
            queryString.Add("openingType", openingType);
            queryString.Add("glassType", glassType);

            Uri uri = new Uri($"{serverUri}{apiPath}?{queryString.ToString()}");

            using var client = new HttpClient();
            byte[] imageBytes = client.GetByteArrayAsync(uri).Result;
            return imageBytes;
        }

        private void loadAllowedMaterials()
        {
            const string apiPath = "/api/windows/drawableWindow";

            if (this.allowedMaterials != null)
                return;

            Uri uri = new Uri($"{serverUri}{apiPath}");

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
