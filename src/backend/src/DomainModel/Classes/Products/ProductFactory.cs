namespace DomainModel.Classes.Products
{
    public static class ProductFactory
    {
        public static IEnumerable<IProduct> GetAll()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(domainAssembly => domainAssembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(AbstractProduct)) && !type.IsAbstract)
                .Select(t =>
                {
                    return (AbstractProduct)Activator.CreateInstance(t)!;
                })
                .OrderBy(m => m.Order);
        }

        public static IProduct GetByCode(string code)
        {
            var t = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(domainAssembly => domainAssembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(AbstractProduct)) && !type.IsAbstract)
                .Single(t => t.Name == code);

            return (AbstractProduct)Activator.CreateInstance(t)!;
        }
    }
}
