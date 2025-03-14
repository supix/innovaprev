namespace DomainModel.Classes.Colors
{
    public static class ColorFactory
    {
        public static IEnumerable<IColor> GetAll()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(domainAssembly => domainAssembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(AbstractColor)) && !type.IsAbstract && type != typeof(NullColor))
                .Select(t =>
                {
                    return (AbstractColor)Activator.CreateInstance(t)!;
                })
                .OrderBy(m => m.Order);
        }

        public static IColor CreateByCode(string code)
        {
            var t = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(domainAssembly => domainAssembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(AbstractColor)) && !type.IsAbstract)
                .Single(t => t.Name == code);

            return (AbstractColor)Activator.CreateInstance(t)!;
        }
    }
}
