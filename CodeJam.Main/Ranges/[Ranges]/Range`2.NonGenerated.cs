﻿using System;
using System.Runtime.CompilerServices;

using CodeJam.Arithmetic;

using JetBrains.Annotations;

using static CodeJam.Ranges.RangeInternal;
using static CodeJam.Targeting.MethodImplOptionsEx;

using SuppressMessageAttribute = System.Diagnostics.CodeAnalysis.SuppressMessageAttribute;

// The file contains members that could not be autogenerated from Range<T>.

namespace CodeJam.Ranges
{
	/// <summary>Describes a range of the values with a key attached.</summary>
	public partial struct Range<T, TKey>
	{
		#region Static members
		private static readonly Func<TKey, TKey, bool> _keyEqualityFunc = Operators<TKey>.AreEqual;

		private static readonly Func<TKey, string, IFormatProvider, string> _formattableCallback =
			CreateFormattableCallback<TKey>();

		#region Predefined values
		/// <summary>Empty range, ∅</summary>
		public static readonly Range<T, TKey> Empty;

		/// <summary>Infinite range, (-∞..+∞)</summary>
		public static readonly Range<T, TKey> Infinite = new Range<T, TKey>(
			RangeBoundaryFrom<T>.NegativeInfinity, RangeBoundaryTo<T>.PositiveInfinity,
			default);
		#endregion

		#endregion

		/// <summary>The value associated with the range.</summary>
		/// <value>The value of the range key.</value>
		// ReSharper disable once ConvertToAutoPropertyWithPrivateSetter
		public TKey Key => _key;

		#region IRangeFactory members
		private Range<T, TKey> CreateRange(RangeBoundaryFrom<T> from, RangeBoundaryTo<T> to) =>
			new Range<T, TKey>(from, to, _key);

		private Range<T, TKey> TryCreateRange(RangeBoundaryFrom<T> from, RangeBoundaryTo<T> to) =>
			Range.TryCreate(from, to, _key);

		[MethodImpl(AggressiveInlining)]
		[Obsolete(SkipsArgValidationObsolete)]
		private Range<T, TKey> CreateUnsafe(RangeBoundaryFrom<T> from, RangeBoundaryTo<T> to) =>
			new Range<T, TKey>(from, to, _key, UnsafeOverload.SkipsArgValidation);

		private Range<T2, TKey> CreateRange<T2>(RangeBoundaryFrom<T2> from, RangeBoundaryTo<T2> to) =>
			new Range<T2, TKey>(from, to, _key);

		private Range<T2, TKey> TryCreateRange<T2>(RangeBoundaryFrom<T2> from, RangeBoundaryTo<T2> to) =>
			Range.TryCreate(from, to, _key);
		#endregion

		#region Operations
		/// <summary>Creates a range without a range key.</summary>
		/// <returns>A new range without a key.</returns>
		[Pure, MethodImpl(AggressiveInlining)]
		public Range<T> WithoutKey() =>
			Range.Create(From, To);
		#endregion

		#region IEquatable<Range<T, TKey>>
		/// <summary>Indicates whether the current range is equal to another.</summary>
		/// <param name="other">An range to compare with this.</param>
		/// <returns>
		/// <c>True</c> if the current range is equal to the <paramref name="other"/> parameter;
		/// otherwise, false.
		/// </returns>
		[Pure]
		[MethodImpl(AggressiveInlining)]
		public bool Equals(Range<T, TKey> other) =>
			_from == other._from && _to == other._to && _keyEqualityFunc(_key, other._key);

		/// <summary>Indicates whether the current range and a specified object are equal.</summary>
		/// <param name="obj">The object to compare with this. </param>
		/// <returns>
		/// <c>True</c> if <paramref name="obj"/> and the current range are the same type
		/// and represent the same value; otherwise, false.
		/// </returns>
		[Pure]
		public override bool Equals(object? obj) => obj is Range<T, TKey> other && Equals(other);

		/// <summary>Returns a hash code for the current range.</summary>
		/// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
		[Pure]
		[SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode", Justification = "Read the comment on the fields.")]
		public override int GetHashCode() =>
			HashCode.Combine(
				_from.GetHashCode(),
				_to.GetHashCode(),
				_key?.GetHashCode() ?? 0);
		#endregion

		#region ToString
		/// <summary>Returns string representation of the range.</summary>
		/// <returns>The string representation of the range.</returns>
		[Pure]
		public override string ToString() =>
			KeyPrefixString + _key + KeySeparatorString +
				(IsEmpty ? EmptyString : _from + SeparatorString + _to);

		/// <summary>
		/// Returns string representation of the range using the specified format string.
		/// If <typeparamref name="T"/> does not implement <seealso cref="IFormattable"/> the format string is ignored.
		/// </summary>
		/// <param name="format">The format string.</param>
		/// <returns>The string representation of the range.</returns>
		[Pure, NotNull]
		public string ToString(string format) => ToString(format, null);

		/// <summary>
		/// Returns string representation of the range using the specified format string.
		/// If <typeparamref name="T"/> does not implement <seealso cref="IFormattable"/> the format string is ignored.
		/// </summary>
		/// <param name="formatProvider">The format provider.</param>
		/// <returns>The string representation of the range.</returns>
		[Pure]
		public string ToString(IFormatProvider formatProvider) => ToString(null, formatProvider);

		/// <summary>
		/// Returns string representation of the range using the specified format string.
		/// If <typeparamref name="T"/> does not implement <seealso cref="IFormattable"/> the format string is ignored.
		/// </summary>
		/// <param name="format">The format string.</param>
		/// <param name="formatProvider">The format provider.</param>
		/// <returns>The string representation of the range.</returns>
		[Pure]
		[SuppressMessage("ReSharper", "ArrangeRedundantParentheses")]
		public string ToString(string format, IFormatProvider formatProvider) =>
			KeyPrefixString + _formattableCallback(_key, null, formatProvider) + KeySeparatorString +
				(IsEmpty
					? EmptyString
					: (_from.ToString(format, formatProvider) + SeparatorString + _to.ToString(format, formatProvider)));
		#endregion
	}
}