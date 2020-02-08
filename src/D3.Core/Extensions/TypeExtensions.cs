namespace D3.Core.Extensions
{
    using System;
    using System.Linq;

    /// <summary>
    /// <seealso cref="Type" /> extension methods
    /// </summary>
    public static class TypeExtensions
    {
        public static bool Implements<TInterface>(this Type type)
        {
            return type.GetInterfaces().Any(i => i == typeof(TInterface));
        }

        public static bool Implements(this Type type, Type interfaceType)
        {
            for (var currentType = type; currentType != null; currentType = currentType.BaseType)
            {
                if (currentType.GetInterfaces().Any(i => i == interfaceType || i.Implements(interfaceType)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}