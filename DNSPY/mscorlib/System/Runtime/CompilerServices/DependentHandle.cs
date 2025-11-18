using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008E8 RID: 2280
	[ComVisible(false)]
	internal struct DependentHandle
	{
		// Token: 0x06005DFE RID: 24062 RVA: 0x0014A32C File Offset: 0x0014852C
		[SecurityCritical]
		public DependentHandle(object primary, object secondary)
		{
			IntPtr intPtr = (IntPtr)0;
			DependentHandle.nInitialize(primary, secondary, out intPtr);
			this._handle = intPtr;
		}

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x06005DFF RID: 24063 RVA: 0x0014A350 File Offset: 0x00148550
		public bool IsAllocated
		{
			get
			{
				return this._handle != (IntPtr)0;
			}
		}

		// Token: 0x06005E00 RID: 24064 RVA: 0x0014A364 File Offset: 0x00148564
		[SecurityCritical]
		public object GetPrimary()
		{
			object obj;
			DependentHandle.nGetPrimary(this._handle, out obj);
			return obj;
		}

		// Token: 0x06005E01 RID: 24065 RVA: 0x0014A37F File Offset: 0x0014857F
		[SecurityCritical]
		public void GetPrimaryAndSecondary(out object primary, out object secondary)
		{
			DependentHandle.nGetPrimaryAndSecondary(this._handle, out primary, out secondary);
		}

		// Token: 0x06005E02 RID: 24066 RVA: 0x0014A390 File Offset: 0x00148590
		[SecurityCritical]
		public void Free()
		{
			if (this._handle != (IntPtr)0)
			{
				IntPtr handle = this._handle;
				this._handle = (IntPtr)0;
				DependentHandle.nFree(handle);
			}
		}

		// Token: 0x06005E03 RID: 24067
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void nInitialize(object primary, object secondary, out IntPtr dependentHandle);

		// Token: 0x06005E04 RID: 24068
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void nGetPrimary(IntPtr dependentHandle, out object primary);

		// Token: 0x06005E05 RID: 24069
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void nGetPrimaryAndSecondary(IntPtr dependentHandle, out object primary, out object secondary);

		// Token: 0x06005E06 RID: 24070
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void nFree(IntPtr dependentHandle);

		// Token: 0x04002A4C RID: 10828
		private IntPtr _handle;
	}
}
