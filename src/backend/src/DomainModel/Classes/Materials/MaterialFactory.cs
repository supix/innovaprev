namespace DomainModel.Classes.Materials
{
    public static class MaterialFactory
    {
        public static IMaterial CreateByCode(string code, long m1, long m2, bool opaqueGlass)
        {
            var t = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(domainAssembly => domainAssembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(AbstractMaterial)) && !type.IsAbstract).Single(t => t.Name == code);

            // Safe guard
            if (t.IsSubclassOf(typeof(SingleDimMaterial)))
            {
                if (m2 != 0)
                    throw new InvalidOperationException($"For a single dimension material m2 must be equal to zero. Material code: {code}");

                return (AbstractMaterial)Activator.CreateInstance(t, m1)!;
            }

            if (t.IsSubclassOf(typeof(DoubleDimMaterial)))
            {
                if (m1 == 0 || m2 == 0)
                    throw new InvalidOperationException($"For a double dimension material m1 and m2 must not be zero. Material code: {code}");

                return (AbstractMaterial)Activator.CreateInstance(t, m1, m2, opaqueGlass)!;
            }

            throw new InvalidOperationException($"Unknown material. Code: {code}");
        }

        public static IEnumerable<IMaterial> GetAll()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(domainAssembly => domainAssembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(AbstractMaterial)) && !type.IsAbstract)
                .Select(t =>
                {
                    if (t.IsSubclassOf(typeof(SingleDimMaterial)))
                    {
                        return (AbstractMaterial)Activator.CreateInstance(t, 0)!;
                    }

                    if (t.IsSubclassOf(typeof(DoubleDimMaterial)))
                    {
                        return (AbstractMaterial)Activator.CreateInstance(t, 0, 0, false)!;
                    }

                    throw new NotImplementedException($"Unhandled material type: {t}");
                })
                .OrderBy(m => m.Order);
        }
    }
}
