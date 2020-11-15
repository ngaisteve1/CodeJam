﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

using JetBrains.Annotations;
using JBNotNullAttribute = JetBrains.Annotations.NotNullAttribute;

// ReSharper disable once CheckNamespace
namespace CodeJam.UseCases.TempDataSamples
{
	[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
	[SuppressMessage("ReSharper", "ArgumentsStyleLiteral")]
	public class TempDataUseCases
	{
		public void CaseA_NoHelper()
		{
			// weird one.

			var fileName = "1.txt";
			Exception deletedEx = null;
			try
			{
				File.Create(fileName).Close();

				Process(fileName);
			}
			finally
			{
				try
				{
					File.Delete(fileName); // we do expect this will throw.
				}
				catch (Exception ex)
				{
					deletedEx = ex;
				}
			}

			if (deletedEx != null)
			{
				HandleDeleteFailure(fileName, deletedEx);
			}
		}

		public void CaseB_LetItCrash()
		{
			// ok if failure is handled by caller
			using (var tempFile = TempData.CreateFile(throwOnDisposeFailure: true))
			{
				Process(tempFile.FileName);
			} // dispose will throw
		}

		public void CaseC_LetItCrashHandle()
		{
			// BAD BAD BAD: we need to store fileName somewhere.
			string fileName = null;
			try
			{
				using (var tempFile = TempData.CreateFile(throwOnDisposeFailure: true))
				{
					fileName = tempFile.FileName;
					Process(tempFile.FileName);
				}
			}
			catch (Exception ex) when (fileName != null) // HACK. Will not work on C#5 or below.
			{
				HandleDeleteFailure(fileName, ex);
			}
		}

		public void CaseD_TryDelete()
		{
			using (var tempFile = TempData.CreateFile())
			{
				Process(tempFile.FileName);

				if (!tempFile.TryClose())
				{
					HandleDeleteFailure(tempFile.FileName, null); // no exception info available.
				}
			}
		}

		public void CaseE_EnsureDeleted()
		{
			// kinda ok
			using (var tempFile = TempData.CreateFile())
			{
				Process(tempFile.FileName);

				try
				{
					tempFile.EnsureDelete();
				}
				catch (Exception ex)
				{
					HandleDeleteFailure(tempFile.FileName, ex);
				}
			}
		}

		public void CaseF_ManualDelete()
		{
			// kinda ok
			using (var tempFile = TempData.CreateFile())
			{
				Process(tempFile.FileName);

				try
				{
					File.Delete(tempFile.FileName);
				}
				catch (Exception ex)
				{
					HandleDeleteFailure(tempFile.FileName, ex);
				}
			}
		}


		public void CaseG_Fallback()
		{
			// The best?
			using (var tempFile = TempData.CreateFile(
				(f, ex) => HandleDeleteFailure(f.FileName, ex)))
			{
				Process(tempFile.FileName);
			}
		}

		private void Process(string tempFile) { }

		private void HandleDeleteFailure(string fileName, Exception exception) { }
	}

	// Stub implementation. Unusable by design.
	[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
	public class TempData : IDisposable
	{
		[JBNotNull] public static TempData CreateFile() => new TempData();
		[JBNotNull] public static TempData CreateFile(bool throwOnDisposeFailure) => new TempData();
		[JBNotNull] public static TempData CreateFile([JBNotNull] Action<TempData, Exception> deleteFallback) => new TempData();

		public string FileName => null;

		public void EnsureDelete()
		{
			throw new IOException("Cannot delete the file.");
		}

		public bool TryClose() => false;

		public void Dispose()
		{
			// ok or throw
		}
	}
}