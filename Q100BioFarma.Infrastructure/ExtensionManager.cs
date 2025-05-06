using System.Collections.Concurrent;
using System.Reflection;

namespace Q100BioFarma.Infrastructur;

public static class ExtensionManager
{
    private static ConcurrentDictionary<Type, IEnumerable<Type>> types;

    public static IEnumerable<Assembly> Assemblies { get; private set; }

    public static void SetAssemblies(IEnumerable<Assembly> assemblies)
    {
        Assemblies = assemblies;
        types = new ConcurrentDictionary<Type, IEnumerable<Type>>();
    }

    public static Type GetImplementation<T>(bool useCaching = false)
    {
        return GetImplementation<T>(null, useCaching);
    }

    public static Type GetImplementation<T>(Func<Assembly, bool> predicate, bool useCaching = false)
    {
        return GetImplementations<T>(predicate, useCaching).FirstOrDefault();
    }

    public static IEnumerable<Type> GetImplementations<T>(bool useCaching = false)
    {
        return GetImplementations<T>(null, useCaching);
    }

    public static IEnumerable<Type> GetImplementations<T>(Func<Assembly, bool> predicate, bool useCaching = false)
    {
        var type = typeof(T);

        if (useCaching && types.ContainsKey(type))
            return types[type];

        var implementations = new List<Type>();

        foreach (var assembly in GetAssemblies(predicate))
        {
            foreach (var exportedType in assembly.GetExportedTypes())
            {
                if (type.GetTypeInfo().IsAssignableFrom(exportedType) && exportedType.GetTypeInfo().IsClass)
                {
                    implementations.Add(exportedType);
                }
            }
        }

        if (useCaching)
        {
            types[type] = implementations;
        }

        return implementations;
    }

    public static T GetInstance<T>(bool useCaching = false)
    {
        return GetInstance<T>(null, useCaching, new object[] { });
    }

    public static T GetInstance<T>(bool useCaching = false, params object[] args)
    {
        return GetInstance<T>(null, useCaching, args);
    }

    public static T GetInstance<T>(Func<Assembly, bool> predicate, bool useCaching = false)
    {
        return GetInstances<T>(predicate, useCaching).FirstOrDefault();
    }

    public static T GetInstance<T>(Func<Assembly, bool> predicate, bool useCaching = false, params object[] args)
    {
        return GetInstances<T>(predicate, useCaching, args).FirstOrDefault();
    }

    public static IEnumerable<T> GetInstances<T>(bool useCaching = false)
    {
        return GetInstances<T>(null, useCaching, new object[] { });
    }

    public static IEnumerable<T> GetInstances<T>(bool useCaching = false, params object[] args)
    {
        return GetInstances<T>(null, useCaching, args);
    }

    public static IEnumerable<T> GetInstances<T>(Func<Assembly, bool> predicate, bool useCaching = false)
    {
        return GetInstances<T>(predicate, useCaching, new object[] { });
    }

    public static IEnumerable<T> GetInstances<T>(
        Func<Assembly, bool> predicate,
        bool useCaching = false,
        params object[] args)
    {
        var instances = new List<T>();

        foreach (var implementation in GetImplementations<T>(predicate, useCaching))
        {
            if (!implementation.GetTypeInfo().IsAbstract)
            {
                var instance = (T)Activator.CreateInstance(implementation, args);

                instances.Add(instance);
            }
        }

        return instances;
    }

    private static IEnumerable<Assembly> GetAssemblies(Func<Assembly, bool> predicate)
    {
        if (predicate == null)
        {
            return Assemblies;
        }

        return Assemblies.Where(predicate);
    }
}