using System.Net;
using DomainModel.Services;

namespace ProductImages
{
    internal class FromResources : IProductImageProvider
    {
        public byte[] Get(string productCode, bool isThumb)
        {
            switch (productCode)
            {
                case "ELA":
                    return isThumb ? ProductImages.RH36Y_thumb : ProductImages.RH36Y;
                case "RALT":
                    return isThumb ? ProductImages.no_image_thumb : ProductImages.no_image;
                case "AATT":
                    return isThumb ? ProductImages.T2S3W_thumb : ProductImages.T2S3W;
                case "AALAM":
                    return isThumb ? ProductImages.T2S3W_thumb : ProductImages.T2S3W;
                case "IPC":
                    return isThumb ? ProductImages.U651R_thumb : ProductImages.U651R;
                case "IPN":
                    return isThumb ? ProductImages.H2G71_thumb : ProductImages.H2G71;
                case "SP":
                    return isThumb ? ProductImages.no_image : ProductImages.no_image;
                default:
                    throw new InvalidOperationException($"Unexisting product code: {productCode}");
            }
        }
    }
}
