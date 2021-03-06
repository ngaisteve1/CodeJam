﻿using System;
using System.Collections.Generic;

using CodeJam.Ranges;

using JetBrains.Annotations;

using Range = CodeJam.Ranges.Range;

namespace CodeJam
{
	public partial class Algorithms
	{
		/// <summary>
		/// Returns the tuple of [i, j] where
		///		i is the smallest index in the range [0, list.Count - 1] such that list[i] >= value or list.Count if no such i exists
		///		j is the smallest index in the range [0, list.Count - 1] such that list[i] > value or list.Count if no such j exists
		/// </summary>
		/// <typeparam name="TElement">The list element type</typeparam>
		/// <typeparam name="TValue">The type of the value</typeparam>
		/// <param name="list">The sorted list</param>
		/// <param name="value">The value to compare</param>
		/// <param name="comparer">The function with the Comparer&lt;T&gt;.Compare semantics</param>
		/// <returns>The tuple of lower bound and upper bound for the value</returns>
#pragma warning disable CA1062 // Validate arguments of public methods
		[Pure, System.Diagnostics.Contracts.Pure]
		public static Range<int> EqualRange<TElement, TValue>(
			[InstantHandle] this IList<TElement> list,
			TValue value,
			[InstantHandle] Func<TElement, TValue, int> comparer) =>
				EqualRange(list, value, 0, list.Count, comparer);
#pragma warning restore CA1062 // Validate arguments of public methods

		/// <summary>
		/// Returns the tuple of [i, j] where
		///		i is the smallest index in the range [startIndex, list.Count - 1] such that list[i] >= value or list.Count if no such i exists
		///		j is the smallest index in the range [startIndex, list.Count - 1] such that list[i] > value or list.Count if no such j exists
		/// </summary>
		/// <typeparam name="TElement">The list element type</typeparam>
		/// <typeparam name="TValue">The type of the value</typeparam>
		/// <param name="list">The sorted list</param>
		/// <param name="value">The value to compare</param>
		/// <param name="startIndex">The minimum index</param>
		/// <param name="comparer">The function with the Comparer&lt;T&gt;.Compare semantics</param>
		/// <returns>The tuple of lower bound and upper bound for the value</returns>
#pragma warning disable CA1062 // Validate arguments of public methods
		[Pure, System.Diagnostics.Contracts.Pure]
		public static Range<int> EqualRange<TElement, TValue>(
			[InstantHandle] this IList<TElement> list,
			TValue value,
			[NonNegativeValue] int startIndex,
			[InstantHandle] Func<TElement, TValue, int> comparer) =>
				EqualRange(list, value, startIndex, list.Count, comparer);
#pragma warning restore CA1062 // Validate arguments of public methods

		/// <summary>
		/// Returns the tuple of [i, j] where
		///		i is the smallest index in the range [startIndex, endIndex - 1] such that list[i] >= value or endIndex if no such i exists
		///		j is the smallest index in the range [startIndex, endIndex - 1] such that list[i] > value or endIndex if no such j exists
		/// </summary>
		/// <typeparam name="TElement">The list element type</typeparam>
		/// <typeparam name="TValue">The type of the value</typeparam>
		/// <param name="list">The sorted list</param>
		/// <param name="value">The value to compare</param>
		/// <param name="startIndex">The minimum index</param>
		/// <param name="endIndex">The upper bound for the index (not included)</param>
		/// <param name="comparer">The function with the Comparer&lt;T&gt;.Compare semantics</param>
		/// <returns>The tuple of lower bound and upper bound for the value</returns>
		[Pure, System.Diagnostics.Contracts.Pure]
		public static Range<int> EqualRange<TElement, TValue>(
			[InstantHandle] this IList<TElement> list,
			TValue value,
			[NonNegativeValue] int startIndex,
			[NonNegativeValue] int endIndex,
			[InstantHandle] Func<TElement, TValue, int> comparer)
		{
			Code.NotNull(list, nameof(list));
			Code.NotNull(comparer, nameof(comparer));
			ValidateIndicesRange(startIndex, endIndex, list.Count);

			var upperBoundStartIndex = startIndex;
			var upperBoundEndIndex = endIndex;

			// the loop locates the lower bound at the same time restricting the range for upper bound search
			while (startIndex < endIndex)
			{
				var median = startIndex + (endIndex - startIndex) / 2;
				var compareResult = comparer(list[median], value);
				switch (compareResult)
				{
					case < 0:
						startIndex = median + 1;
						upperBoundStartIndex = startIndex;
						break;
					case 0:
						endIndex = median;
						upperBoundStartIndex = endIndex + 1;
						break;
					default:
						endIndex = median;
						upperBoundEndIndex = endIndex;
						break;
				}
			}
			return Range.Create(startIndex, UpperBoundCore(list, value, upperBoundStartIndex, upperBoundEndIndex, comparer));
		}
	}
}