// <copyright file="TypeExtensions.cs" company="D-Cubed">
// Copyright (c) D-Cubed. All rights reserved.
// </copyright>

namespace D3.Core.Extensions
{
    using System;
    using System.Linq;

    /// <summary>
    /// <seealso cref="Type" /> extension methods.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Check if the specified type implements <typeparamref name="TInterface"/>.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <param name="type">The <seealso cref="Type"/> to check.</param>
        /// <returns><c>true</c> if the specified type implements interface <typeparamref name="TInterface"/>, <c>false</c> otherwise.</returns>
        public static bool Implements<TInterface>(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.GetInterfaces().Any(i => i == typeof(TInterface));
        }

        /// <summary>
        /// Check if the specified type implements <typeparamref name="TInterface"/>.
        /// </summary>
        /// <param name="type">The <seealso cref="Type"/> to check.</param>
        /// <param name="interfaceType">The type of the interface.</param>
        /// <returns><c>true</c> if the specified type implements interface <typeparamref name="TInterface"/>, <c>false</c> otherwise.</returns>
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
