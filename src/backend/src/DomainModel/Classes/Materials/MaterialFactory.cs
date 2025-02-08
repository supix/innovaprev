
namespace DomainModel.Classes.Materials
{
    public static class MaterialFactory
    {
        public static IMaterial CreateByCode(string code, params long[] measures)
        {
            var m_type = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(domainAssembly => domainAssembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(AbstractMaterial)) && !type.IsAbstract).Single(t => t.Name == code);

            // Safe guard
            if (m_type.IsSubclassOf(typeof(SingleDimMaterial)) && measures.Length != 1)
            {
                throw new InvalidOperationException($"Single dimension material must be created with a single measure. Material code: {code}");
            }
            
            if (m_type.IsSubclassOf(typeof(DoubleDimMaterial)) && measures.Length != 2)
            {
                throw new InvalidOperationException($"Double dimension material must be created with just two measures. Material code: {code}");
            }

            return (AbstractMaterial)Activator.CreateInstance(m_type, measures)!;
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
                        return (AbstractMaterial)Activator.CreateInstance(t, 0, 0)!;
                    }

                    throw new NotImplementedException($"Unhandled material type: {t}");
                })
                .OrderBy(m => m.Order);
        }
    }
}
