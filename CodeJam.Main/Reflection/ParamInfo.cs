﻿
using JetBrains.Annotations;

namespace CodeJam.Reflection
{
	/// <summary>
	/// Parameter data for CreateInstance method.
	/// </summary>
	[PublicAPI]
	public class ParamInfo
	{
		/// <summary>Initializes a new instance of the <see cref="ParamInfo"/> class.</summary>
		/// <param name="name">Name of the parameter.</param>
		/// <param name="value">Value of the parameter.</param>
		/// <param name="required"><c>True</c> if parameter required.</param>
		public ParamInfo([NotNull] string name, [CanBeNull] object? value, bool required = true)
		{
			Code.NotNullNorWhiteSpace(name, nameof(name));

			Name = name;
			Value = value;
			Required = required;
		}

		/// <summary>Initializes a new instance of the <see cref="ParamInfo"/> class.</summary>
		/// <param name="name">Name of the parameter.</param>
		/// <param name="value">Value of the parameter.</param>
		/// <param name="required"><c>True</c> if parameter required.</param>
		/// <returns>Instance of <see cref="ParamInfo"/>.</returns>
		[Pure]
		[NotNull]
		public static ParamInfo Param([NotNull] string name, [CanBeNull] object? value, bool required = true) =>
			new ParamInfo(name, value, required);

		/// <summary>
		/// Parameter name.
		/// </summary>
		[NotNull]
		public string Name { get; set; }

		/// <summary>
		/// Parameter value.
		/// </summary>
		[CanBeNull]
		public object? Value { get; set; }

		/// <summary>
		/// True, if parameter required.
		/// </summary>
		public bool Required { get; set; }
	}
}