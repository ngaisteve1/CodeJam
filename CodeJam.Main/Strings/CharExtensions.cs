﻿using System;
using System.Globalization;

using JetBrains.Annotations;

namespace CodeJam.Strings
{
	/// <summary>
	/// <see cref="char"/> structure extensions.
	/// </summary>
	[PublicAPI]
	public static partial class CharExtensions
	{
		/// <summary>
		/// Converts the value of a Unicode character to its lowercase equivalent.
		/// </summary>
		/// <param name="chr">The Unicode character to convert.</param>
		/// <returns>
		/// The lowercase equivalent of <paramref name="chr"/>, or the unchanged value of <paramref name="chr"/>,
		/// if <paramref name="chr"/> is already lowercase or not alphabetic.
		/// </returns>
		[Pure]
		public static char ToLower(this char chr) => char.ToLower(chr);

#if !LESSTHAN_NETSTANDARD20 && !LESSTHAN_NETCOREAPP20

		/// <summary>
		/// Converts the value of a Unicode character to its lowercase equivalent.
		/// </summary>
		/// <param name="chr">The Unicode character to convert.</param>
		/// <param name="culture">An object that supplies culture-specific casing rules.</param>
		/// <returns>
		/// The lowercase equivalent of <paramref name="chr"/>, modified according to <paramref name="culture"/>,
		/// or the unchanged value of <paramref name="chr"/>, if <paramref name="chr"/> is already lowercase or not
		/// alphabetic.
		/// </returns>
		[Pure]
		public static char ToLower(this char chr, [NotNull] CultureInfo culture) => char.ToLower(chr, culture);

#endif

		/// <summary>
		/// Converts the value of a Unicode character to its lowercase equivalent using the casing rules of the invariant
		/// culture.
		/// </summary>
		/// <param name="chr">The Unicode character to convert.</param>
		/// <returns>
		/// The lowercase equivalent of <paramref name="chr"/>, or the unchanged value of <paramref name="chr"/>,
		/// if <paramref name="chr"/> is already lowercase or not alphabetic.
		/// </returns>
		[Pure]
		public static char ToLowerInvariant(this char chr) => char.ToLowerInvariant(chr);

		/// <summary>
		/// Converts the value of a Unicode character to its uppercase equivalent.
		/// </summary>
		/// <param name="chr">The Unicode character to convert.</param>
		/// <returns>
		/// The uppercase equivalent of <paramref name="chr"/>, or the unchanged value of <paramref name="chr"/>,
		/// if <paramref name="chr"/> is already uppercase or not alphabetic.
		/// </returns>
		[Pure]
		public static char ToUpper(this char chr) => char.ToUpper(chr);

#if !LESSTHAN_NETSTANDARD20 && !LESSTHAN_NETCOREAPP20

		/// <summary>
		/// Converts the value of a Unicode character to its uppercase equivalent.
		/// </summary>
		/// <param name="chr">The Unicode character to convert.</param>
		/// <param name="culture">An object that supplies culture-specific casing rules.</param>
		/// <returns>
		/// The uppercase equivalent of <paramref name="chr"/>, modified according to <paramref name="culture"/>,
		/// or the unchanged value of <paramref name="chr"/>,  if <paramref name="chr"/> is already uppercase or not
		/// alphabetic.
		/// </returns>
		[Pure]
		public static char ToUpper(this char chr, [NotNull] CultureInfo culture) => char.ToUpper(chr, culture);

#endif

		/// <summary>
		/// Converts the value of a Unicode character to its uppercase equivalent using the casing rules of the invariant
		/// culture.
		/// </summary>
		/// <param name="chr">The Unicode character to convert.</param>
		/// <returns>
		/// The uppercase equivalent of <paramref name="chr"/>, or the unchanged value of <paramref name="chr"/>,
		/// if <paramref name="chr"/> is already uppercase or not alphabetic.
		/// </returns>
		[Pure]
		public static char ToUpperInvariant(this char chr) => char.ToUpperInvariant(chr);
	}
}