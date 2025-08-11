namespace DomainModel.Classes.Frames
{
    public static class FrameFactory
    {
        public static IEnumerable<IFrame> GetAll()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(domainAssembly => domainAssembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(AbstractFrame)) && !type.IsAbstract)
                .Select(t =>
                {
                    return (AbstractFrame)Activator.CreateInstance(t)!;
                })
                .OrderBy(m => m.Order);
        }

        public static IFrame CreateByCode(string code)
        {
            var t = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(domainAssembly => domainAssembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(AbstractFrame)) && !type.IsAbstract)
                .Single(t => t.Name == code);

            return (AbstractFrame)Activator.CreateInstance(t)!;
        }
    }
}
