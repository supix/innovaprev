﻿using DomainModel.Services;
using DomainModel.Services.CollectionsProvider;
using DomainModel.Services.PriceCalculator;
using PdfQuote;
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
        }
    }
}
