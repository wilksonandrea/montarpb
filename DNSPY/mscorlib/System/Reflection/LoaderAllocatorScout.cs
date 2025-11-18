using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005F3 RID: 1523
	internal sealed class LoaderAllocatorScout
	{
		// Token: 0x06004677 RID: 18039
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern bool Destroy(IntPtr nativeLoaderAllocator);

		// Token: 0x06004678 RID: 18040 RVA: 0x001024D0 File Offset: 0x001006D0
		[SecuritySafeCritical]
		~LoaderAllocatorScout()
		{
			if (!this.m_nativeLoaderAllocator.IsNull())
			{
				if (!Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload() && !LoaderAllocatorScout.Destroy(this.m_nativeLoaderAllocator))
				{
					GC.ReRegisterForFinalize(this);
				}
			}
		}

		// Token: 0x06004679 RID: 18041 RVA: 0x0010252C File Offset: 0x0010072C
		public LoaderAllocatorScout()
		{
		}

		// Token: 0x04001CDB RID: 7387
		internal IntPtr m_nativeLoaderAllocator;
	}
}
