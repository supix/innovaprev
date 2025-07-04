﻿using DomainModel.Classes;
using DomainModel.Services;
using DomainModel.Services.CollectionsProvider;
using DomainModel.Services.PriceCalculator;
using DomainModel.Services.WindowImageRenderer;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PdfQuote
{
    public class Generator : IPdfReportGenerator
    {
        private readonly IPriceCalculator priceCalculator;
        private readonly ICollectionProvider collectionProvider;
        private readonly IProductImageProvider imageProvider;
        private readonly IWindowImageRenderer wir;

        public Generator(IPriceCalculator priceCalculator, ICollectionProvider collectionProvider, IProductImageProvider imageProvider, IWindowImageRenderer wir)
        {
            this.priceCalculator = priceCalculator ?? throw new ArgumentNullException(nameof(priceCalculator));
            this.collectionProvider = collectionProvider ?? throw new ArgumentNullException(nameof(collectionProvider));
            this.imageProvider = imageProvider ?? throw new ArgumentNullException(nameof(imageProvider));
            this.wir = wir ?? throw new ArgumentNullException(nameof(wir));
        }

        public byte[] Generate(Project project)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var priceInfo = priceCalculator.getPrices(project.ProductData, project.WindowsData, project.CustomData);
            var doc = new QuoteTemplate(project, priceInfo, collectionProvider, imageProvider, wir);
            return Document.Create(container => doc.Compose(container)).GeneratePdf();
        }
    }
}