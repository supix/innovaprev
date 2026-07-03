export const sharedEnvironment = {
  api: {
    baseUrl: 'api/',
    windowRendererBaseUrl: 'window-api/',
    windowRendererEndpoints: {
      windows: 'windows'
    },
    endpoints: {
      getQuote: 'getQuote',
      downloadPdf: 'pdf',
      collections: 'collections',
      productImage: 'ProductImage',
      energyCollections: 'energy/collections',
      energyMunicipalities: 'energy/municipalities',
      energyCalculate: 'energy/calculate'
    }
  }
};
