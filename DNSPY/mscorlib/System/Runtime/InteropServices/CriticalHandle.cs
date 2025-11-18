using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000947 RID: 2375
	[SecurityCritical]
	[__DynamicallyInvokable]
	[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	public abstract class CriticalHandle : CriticalFinalizerObject, IDisposable
	{
		// Token: 0x06006073 RID: 24691 RVA: 0x0014BF32 File Offset: 0x0014A132
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected CriticalHandle(IntPtr invalidHandleValue)
		{
			this.handle = invalidHandleValue;
			this._isClosed = false;
		}

		// Token: 0x06006074 RID: 24692 RVA: 0x0014BF48 File Offset: 0x0014A148
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		~CriticalHandle()
		{
			this.Dispose(false);
		}

		// Token: 0x06006075 RID: 24693 RVA: 0x0014BF78 File Offset: 0x0014A178
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private void Cleanup()
		{
			if (this.IsClosed)
			{
				return;
			}
			this._isClosed = true;
			if (this.IsInvalid)
			{
				return;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!this.ReleaseHandle())
			{
				this.FireCustomerDebugProbe();
			}
			Marshal.SetLastWin32Error(lastWin32Error);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06006076 RID: 24694
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void FireCustomerDebugProbe();

		// Token: 0x06006077 RID: 24695 RVA: 0x0014BFBE File Offset: 0x0014A1BE
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected void SetHandle(IntPtr handle)
		{
			this.handle = handle;
		}

		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x06006078 RID: 24696 RVA: 0x0014BFC7 File Offset: 0x0014A1C7
		[__DynamicallyInvokable]
		public bool IsClosed
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			get
			{
				return this._isClosed;
			}
		}

		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x06006079 RID: 24697
		[__DynamicallyInvokable]
		public abstract bool IsInvalid
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x0600607A RID: 24698 RVA: 0x0014BFCF File Offset: 0x0014A1CF
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x0600607B RID: 24699 RVA: 0x0014BFD8 File Offset: 0x0014A1D8
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600607C RID: 24700 RVA: 0x0014BFE1 File Offset: 0x0014A1E1
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			this.Cleanup();
		}

		// Token: 0x0600607D RID: 24701 RVA: 0x0014BFE9 File Offset: 0x0014A1E9
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public void SetHandleAsInvalid()
		{
			this._isClosed = true;
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600607E RID: 24702
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		protected abstract bool ReleaseHandle();

		// Token: 0x04002B46 RID: 11078
		protected IntPtr handle;

		// Token: 0x04002B47 RID: 11079
		private bool _isClosed;
	}
}
