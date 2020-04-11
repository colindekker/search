// <copyright file="StringExtensions.cs" company="D-Cubed">
// Copyright (c) D-Cubed. All rights reserved.
// </copyright>

namespace D3.Core.Extensions
{
    using System.Globalization;

    /// <summary>
    /// <c>string</c> extension methods.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>Capitalizes the specified string.</summary>
        /// <param name="input">The string to capitalize.</param>
        /// <returns>The <paramref name="input"/> string with the first character in uppercase.</returns>
        public static string Capitalize(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            return char.ToUpper(input[0], CultureInfo.CurrentCulture) + input.Substring(1);
        }
    }
}
