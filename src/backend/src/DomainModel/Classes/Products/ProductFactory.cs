using DomainModel.Classes.Colors;

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
                    if (typeof(PvcAbstractProduct).IsAssignableFrom(t))
                        return (AbstractProduct)Activator.CreateInstance(t, new NullColor())!;
                    else
                    if (typeof(WoodAbstractProduct).IsAssignableFrom(t))
                        return (AbstractProduct)Activator.CreateInstance(t, new NullColor(), new NullColor())!;
                    else
                        throw new InvalidOperationException($"Unable to create product: {t.Name}");
                })
                .OrderBy(m => m.Order);
        }

        public static IProduct CreateByCode(string code)
        {
            var t = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(domainAssembly => domainAssembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(AbstractProduct)) && !type.IsAbstract)
                .Single(t => t.Name == code);

            if (typeof(PvcAbstractProduct).IsAssignableFrom(t))
                return (AbstractProduct)Activator.CreateInstance(t, new NullColor())!;
            else
            if (typeof(WoodAbstractProduct).IsAssignableFrom(t))
                return (AbstractProduct)Activator.CreateInstance(t, new NullColor(), new NullColor())!;
            else
                throw new InvalidOperationException($"Unable to create product: {t.Name}");
        }
    }
}
