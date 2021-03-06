﻿using System;
using System.Collections.Generic;

using JetBrains.Annotations;

namespace CodeJam
{
	public partial class Algorithms
	{
		/// <summary>
		/// Returns the minimum index i in the range [0, list.Count - 1] such that list[i] >= value
		/// or list.Count if no such i exists
		/// </summary>
		/// <typeparam name="TElement">
		/// The list element type
		/// <remarks>Should implement IComparable&lt;TValue&gt;</remarks>
		/// </typeparam>
		/// <typeparam name="TValue">The type of the value</typeparam>
		/// <param name="list">The sorted list</param>
		/// <param name="value">The value to compare</param>
		/// <returns>The lower bound for the value</returns>
#pragma warning disable CA1062 // Validate arguments of public methods
		[Pure, System.Diagnostics.Contracts.Pure]
		public static int LowerBound<TElement, TValue>(
			[InstantHandle] this IList<TElement> list,
			TValue value)
			where TElement : IComparable<TValue> =>
				list.LowerBound(value, 0);
#pragma warning restore CA1062 // Validate arguments of public methods

		/// <summary>
		/// Returns the minimum index i in the range [startIndex, list.Count - 1] such that list[i] >= value
		/// or list.Count if no such i exists
		/// </summary>
		/// <typeparam name="TElement">The list element type
		/// <remarks>Should implement IComparable&lt;TValue&gt;</remarks>
		/// </typeparam>
		/// <typeparam name="TValue">The type of the value</typeparam>
		/// <param name="list">The sorted list</param>
		/// <param name="value">The value to compare</param>
		/// <param name="startIndex">The minimum index</param>
		/// <returns>The lower bound for the value</returns>
#pragma warning disable CA1062 // Validate arguments of public methods
		[Pure, System.Diagnostics.Contracts.Pure]
		public static int LowerBound<TElement, TValue>(
			[InstantHandle] this IList<TElement> list,
			TValue value,
			[NonNegativeValue] int startIndex)
			where TElement : IComparable<TValue> =>
				list.LowerBound(value, startIndex, list.Count);
#pragma warning restore CA1062 // Validate arguments of public methods

		/// <summary>
		/// Returns the minimum index i in the range [startIndex, endIndex - 1] such that list[i] >= value
		/// or endIndex if no such i exists
		/// </summary>
		/// <typeparam name="TElement">
		/// The list element type
		/// <remarks>Should implement IComparable&lt;TValue&gt;</remarks>
		/// </typeparam>
		/// <typeparam name="TValue">The type of the value</typeparam>
		/// <param name="list">The sorted list</param>
		/// <param name="value">The value to compare</param>
		/// <param name="startIndex">The minimum index</param>
		/// <param name="endIndex">The upper bound for the index (not included)</param>
		/// <returns>The lower bound for the value</returns>
		[Pure, System.Diagnostics.Contracts.Pure]
		public static int LowerBound<TElement, TValue>(
			[InstantHandle] this IList<TElement> list,
			TValue value,
			[NonNegativeValue] int startIndex,
			[NonNegativeValue] int endIndex)
			where TElement : IComparable<TValue>
		{
			Code.NotNull(list, nameof(list));
			ValidateIndicesRange(startIndex, endIndex, list.Count);
			while (startIndex < endIndex)
			{
				var median = startIndex + (endIndex - startIndex) / 2;
				var compareResult = list[median].CompareTo(value);
				if (compareResult >= 0)
				{
					endIndex = median;
				}
				else
				{
					startIndex = median + 1;
				}
			}
			return startIndex;
		}
	}
}