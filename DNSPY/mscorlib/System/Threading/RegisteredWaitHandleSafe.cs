using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32;

namespace System.Threading
{
	// Token: 0x0200051D RID: 1309
	internal sealed class RegisteredWaitHandleSafe : CriticalFinalizerObject
	{
		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06003DD0 RID: 15824 RVA: 0x000E70A0 File Offset: 0x000E52A0
		private static IntPtr InvalidHandle
		{
			[SecuritySafeCritical]
			get
			{
				return Win32Native.INVALID_HANDLE_VALUE;
			}
		}

		// Token: 0x06003DD1 RID: 15825 RVA: 0x000E70A7 File Offset: 0x000E52A7
		internal RegisteredWaitHandleSafe()
		{
			this.registeredWaitHandle = RegisteredWaitHandleSafe.InvalidHandle;
		}

		// Token: 0x06003DD2 RID: 15826 RVA: 0x000E70BA File Offset: 0x000E52BA
		internal IntPtr GetHandle()
		{
			return this.registeredWaitHandle;
		}

		// Token: 0x06003DD3 RID: 15827 RVA: 0x000E70C2 File Offset: 0x000E52C2
		internal void SetHandle(IntPtr handle)
		{
			this.registeredWaitHandle = handle;
		}

		// Token: 0x06003DD4 RID: 15828 RVA: 0x000E70CC File Offset: 0x000E52CC
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal void SetWaitObject(WaitHandle waitObject)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				this.m_internalWaitObject = waitObject;
				if (waitObject != null)
				{
					this.m_internalWaitObject.SafeWaitHandle.DangerousAddRef(ref this.bReleaseNeeded);
				}
			}
		}

		// Token: 0x06003DD5 RID: 15829 RVA: 0x000E7114 File Offset: 0x000E5314
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal bool Unregister(WaitHandle waitObject)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				bool flag2 = false;
				do
				{
					if (Interlocked.CompareExchange(ref this.m_lock, 1, 0) == 0)
					{
						flag2 = true;
						try
						{
							if (this.ValidHandle())
							{
								flag = RegisteredWaitHandleSafe.UnregisterWaitNative(this.GetHandle(), (waitObject == null) ? null : waitObject.SafeWaitHandle);
								if (flag)
								{
									if (this.bReleaseNeeded)
									{
										this.m_internalWaitObject.SafeWaitHandle.DangerousRelease();
										this.bReleaseNeeded = false;
									}
									this.SetHandle(RegisteredWaitHandleSafe.InvalidHandle);
									this.m_internalWaitObject = null;
									GC.SuppressFinalize(this);
								}
							}
						}
						finally
						{
							this.m_lock = 0;
						}
					}
					Thread.SpinWait(1);
				}
				while (!flag2);
			}
			return flag;
		}

		// Token: 0x06003DD6 RID: 15830 RVA: 0x000E71D0 File Offset: 0x000E53D0
		private bool ValidHandle()
		{
			return this.registeredWaitHandle != RegisteredWaitHandleSafe.InvalidHandle && this.registeredWaitHandle != IntPtr.Zero;
		}

		// Token: 0x06003DD7 RID: 15831 RVA: 0x000E71F8 File Offset: 0x000E53F8
		[SecuritySafeCritical]
		~RegisteredWaitHandleSafe()
		{
			if (Interlocked.CompareExchange(ref this.m_lock, 1, 0) == 0)
			{
				try
				{
					if (this.ValidHandle())
					{
						RegisteredWaitHandleSafe.WaitHandleCleanupNative(this.registeredWaitHandle);
						if (this.bReleaseNeeded)
						{
							this.m_internalWaitObject.SafeWaitHandle.DangerousRelease();
							this.bReleaseNeeded = false;
						}
						this.SetHandle(RegisteredWaitHandleSafe.InvalidHandle);
						this.m_internalWaitObject = null;
					}
				}
				finally
				{
					this.m_lock = 0;
				}
			}
		}

		// Token: 0x06003DD8 RID: 15832
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WaitHandleCleanupNative(IntPtr handle);

		// Token: 0x06003DD9 RID: 15833
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool UnregisterWaitNative(IntPtr handle, SafeHandle waitObject);

		// Token: 0x04001A10 RID: 6672
		private IntPtr registeredWaitHandle;

		// Token: 0x04001A11 RID: 6673
		private WaitHandle m_internalWaitObject;

		// Token: 0x04001A12 RID: 6674
		private bool bReleaseNeeded;

		// Token: 0x04001A13 RID: 6675
		private volatile int m_lock;
	}
}
