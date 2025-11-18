using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006F1 RID: 1777
	[StructLayout(LayoutKind.Sequential)]
	internal class AssemblyReferenceDependentAssemblyEntry : IDisposable
	{
		// Token: 0x060050B9 RID: 20665 RVA: 0x0011DB48 File Offset: 0x0011BD48
		~AssemblyReferenceDependentAssemblyEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x060050BA RID: 20666 RVA: 0x0011DB78 File Offset: 0x0011BD78
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060050BB RID: 20667 RVA: 0x0011DB81 File Offset: 0x0011BD81
		[SecuritySafeCritical]
		public void Dispose(bool fDisposing)
		{
			if (this.HashValue != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.HashValue);
				this.HashValue = IntPtr.Zero;
			}
			if (fDisposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x060050BC RID: 20668 RVA: 0x0011DBB4 File Offset: 0x0011BDB4
		public AssemblyReferenceDependentAssemblyEntry()
		{
		}

		// Token: 0x04002361 RID: 9057
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Group;

		// Token: 0x04002362 RID: 9058
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Codebase;

		// Token: 0x04002363 RID: 9059
		public ulong Size;

		// Token: 0x04002364 RID: 9060
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr HashValue;

		// Token: 0x04002365 RID: 9061
		public uint HashValueSize;

		// Token: 0x04002366 RID: 9062
		public uint HashAlgorithm;

		// Token: 0x04002367 RID: 9063
		public uint Flags;

		// Token: 0x04002368 RID: 9064
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ResourceFallbackCulture;

		// Token: 0x04002369 RID: 9065
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Description;

		// Token: 0x0400236A RID: 9066
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SupportUrl;

		// Token: 0x0400236B RID: 9067
		public ISection HashElements;
	}
}
