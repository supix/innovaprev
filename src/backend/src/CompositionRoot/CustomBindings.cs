using DomainModel.Services;
using DomainModel.Services.CollectionsProvider;
using DomainModel.Services.PriceCalculator;
using PdfQuote;
using ProductImages;
using SimpleInjector;

namespace CompositionRoot
{
    public static class CustomBindings
    {
        public static void Bind(Container container)
        {
            container.Register<IPdfReportGenerator, Generator>();
            container.Register<IPriceCalculator, PriceCalculatorImpl>();
            container.Register<ICollectionProvider, CollectionProviderImpl>();
            
            var stage = Environment.GetEnvironmentVariable("STAGE");
            if (string.IsNullOrWhiteSpace(stage))
            {
                throw new ArgumentException("STAGE environment variable must be set");
            }

            if (stage == "test")
                container.Register<IImageProvider, ImageProvider_Fake>();
            else
            {
                var imagesBaseUrl = Environment.GetEnvironmentVariable("IMAGES_BASE_URL");
                if (string.IsNullOrWhiteSpace(imagesBaseUrl))
                {
                    throw new ArgumentException("IMAGES_BASE_URL environment variable must be set");
                }
                container.Register<IImageProvider>(() => { return new ImageProvider_FromUrl(imagesBaseUrl); });
            }
        }
    }
}
