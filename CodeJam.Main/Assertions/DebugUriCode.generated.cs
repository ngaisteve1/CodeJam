﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#nullable enable


using static CodeJam.DebugCode;

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using JetBrains.Annotations;

using static CodeJam.Targeting.MethodImplOptionsEx;

namespace CodeJam
{
	/// <summary>URI assertions class.</summary>
	[PublicAPI]
	public static class DebugUriCode
	{
		/// <summary>Asserts that specified URI is well-formed absolute or relative URI.</summary>
		/// <param name="uri">The URI.</param>
		/// <param name="argName">Name of the argument.</param>
		[Conditional(DebugCondition), DebuggerHidden, MethodImpl(AggressiveInlining)]
		[AssertionMethod]
		public static void IsWellFormedUri(
			string uri,
			[InvokerParameterName] string argName)
		{
			Code.NotNullNorEmpty(uri, argName);
			if (!Uri.IsWellFormedUriString(uri, UriKind.RelativeOrAbsolute))
				throw UriCodeExceptions.ArgumentNotWellFormedUri(argName, uri, UriKind.RelativeOrAbsolute);
		}

		/// <summary>Asserts that specified URI is well-formed absolute URI.</summary>
		/// <param name="uri">The URI.</param>
		/// <param name="argName">Name of the argument.</param>
		[Conditional(DebugCondition), DebuggerHidden, MethodImpl(AggressiveInlining)]
		[AssertionMethod]
		public static void IsWellFormedAbsoluteUri(
			string uri,
			[InvokerParameterName] string argName)
		{
			Code.NotNullNorEmpty(uri, argName);
			if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
				throw UriCodeExceptions.ArgumentNotWellFormedUri(argName, uri, UriKind.Absolute);
		}

		/// <summary>Asserts that specified URI is well-formed relative URI.</summary>
		/// <param name="uri">The URI.</param>
		/// <param name="argName">Name of the argument.</param>
		[Conditional(DebugCondition), DebuggerHidden, MethodImpl(AggressiveInlining)]
		[AssertionMethod]
		public static void IsWellFormedRelativeUri(
			string uri,
			[InvokerParameterName] string argName)
		{
			Code.NotNullNorEmpty(uri, argName);
			if (!Uri.IsWellFormedUriString(uri, UriKind.Relative))
				throw UriCodeExceptions.ArgumentNotWellFormedUri(argName, uri, UriKind.Relative);
		}
	}
}