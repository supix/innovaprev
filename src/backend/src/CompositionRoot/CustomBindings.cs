using DomainModel.Services;
using DomainModel.Services.CollectionsProvider;
using DomainModel.Services.PriceCalculator;
using DomainModel.Services.WindowImageRenderer;
using PdfQuote;
using ProductImages;
using SimpleInjector;
using WindowImageRenderer_WebService;

namespace CompositionRoot
{
    public static class CustomBindings
    {
        public static void Bind(Container container)
        {
            container.Register<IPdfReportGenerator, Generator>();
            container.Register<IPriceCalculator, PriceCalculatorImpl>();
            container.Register<ICollectionProvider, CollectionProviderImpl>();

            container.Register<IProductImageProvider, FromResources>();
            container.Register<IWindowImageRenderer>(() => new WindowImageRenderer("http://localhost:3000/api/windows"));
        }
    }
}
