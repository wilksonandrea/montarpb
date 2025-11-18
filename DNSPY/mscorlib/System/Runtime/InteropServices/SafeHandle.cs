using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000959 RID: 2393
	[SecurityCritical]
	[__DynamicallyInvokable]
	[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	public abstract class SafeHandle : CriticalFinalizerObject, IDisposable
	{
		// Token: 0x060061DC RID: 25052 RVA: 0x0014EA73 File Offset: 0x0014CC73
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected SafeHandle(IntPtr invalidHandleValue, bool ownsHandle)
		{
			this.handle = invalidHandleValue;
			this._state = 4;
			this._ownsHandle = ownsHandle;
			if (!ownsHandle)
			{
				GC.SuppressFinalize(this);
			}
			this._fullyInitialized = true;
		}

		// Token: 0x060061DD RID: 25053 RVA: 0x0014EAA0 File Offset: 0x0014CCA0
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		~SafeHandle()
		{
			this.Dispose(false);
		}

		// Token: 0x060061DE RID: 25054
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalFinalize();

		// Token: 0x060061DF RID: 25055 RVA: 0x0014EAD0 File Offset: 0x0014CCD0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected void SetHandle(IntPtr handle)
		{
			this.handle = handle;
		}

		// Token: 0x060061E0 RID: 25056 RVA: 0x0014EAD9 File Offset: 0x0014CCD9
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public IntPtr DangerousGetHandle()
		{
			return this.handle;
		}

		// Token: 0x17001105 RID: 4357
		// (get) Token: 0x060061E1 RID: 25057 RVA: 0x0014EAE1 File Offset: 0x0014CCE1
		[__DynamicallyInvokable]
		public bool IsClosed
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			get
			{
				return (this._state & 1) == 1;
			}
		}

		// Token: 0x17001106 RID: 4358
		// (get) Token: 0x060061E2 RID: 25058
		[__DynamicallyInvokable]
		public abstract bool IsInvalid
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x060061E3 RID: 25059 RVA: 0x0014EAEE File Offset: 0x0014CCEE
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x060061E4 RID: 25060 RVA: 0x0014EAF7 File Offset: 0x0014CCF7
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060061E5 RID: 25061 RVA: 0x0014EB00 File Offset: 0x0014CD00
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.InternalDispose();
				return;
			}
			this.InternalFinalize();
		}

		// Token: 0x060061E6 RID: 25062
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalDispose();

		// Token: 0x060061E7 RID: 25063
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetHandleAsInvalid();

		// Token: 0x060061E8 RID: 25064
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		protected abstract bool ReleaseHandle();

		// Token: 0x060061E9 RID: 25065
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DangerousAddRef(ref bool success);

		// Token: 0x060061EA RID: 25066
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DangerousRelease();

		// Token: 0x04002B87 RID: 11143
		protected IntPtr handle;

		// Token: 0x04002B88 RID: 11144
		private int _state;

		// Token: 0x04002B89 RID: 11145
		private bool _ownsHandle;

		// Token: 0x04002B8A RID: 11146
		private bool _fullyInitialized;
	}
}
