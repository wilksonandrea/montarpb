using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004FD RID: 1277
	public class HostExecutionContextManager
	{
		// Token: 0x06003C4C RID: 15436
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HostSecurityManagerPresent();

		// Token: 0x06003C4D RID: 15437
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int ReleaseHostSecurityContext(IntPtr context);

		// Token: 0x06003C4E RID: 15438
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int CloneHostSecurityContext(SafeHandle context, SafeHandle clonedContext);

		// Token: 0x06003C4F RID: 15439
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int CaptureHostSecurityContext(SafeHandle capturedContext);

		// Token: 0x06003C50 RID: 15440
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int SetHostSecurityContext(SafeHandle context, bool fReturnPrevious, SafeHandle prevContext);

		// Token: 0x06003C51 RID: 15441 RVA: 0x000E3E0C File Offset: 0x000E200C
		[SecurityCritical]
		internal static bool CheckIfHosted()
		{
			if (!HostExecutionContextManager._fIsHostedChecked)
			{
				HostExecutionContextManager._fIsHosted = HostExecutionContextManager.HostSecurityManagerPresent();
				HostExecutionContextManager._fIsHostedChecked = true;
			}
			return HostExecutionContextManager._fIsHosted;
		}

		// Token: 0x06003C52 RID: 15442 RVA: 0x000E3E34 File Offset: 0x000E2034
		[SecuritySafeCritical]
		public virtual HostExecutionContext Capture()
		{
			HostExecutionContext hostExecutionContext = null;
			if (HostExecutionContextManager.CheckIfHosted())
			{
				IUnknownSafeHandle unknownSafeHandle = new IUnknownSafeHandle();
				hostExecutionContext = new HostExecutionContext(unknownSafeHandle);
				HostExecutionContextManager.CaptureHostSecurityContext(unknownSafeHandle);
			}
			return hostExecutionContext;
		}

		// Token: 0x06003C53 RID: 15443 RVA: 0x000E3E60 File Offset: 0x000E2060
		[SecurityCritical]
		public virtual object SetHostExecutionContext(HostExecutionContext hostExecutionContext)
		{
			if (hostExecutionContext == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotNewCaptureContext"));
			}
			HostExecutionContextSwitcher hostExecutionContextSwitcher = new HostExecutionContextSwitcher();
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			hostExecutionContextSwitcher.executionContext = mutableExecutionContext;
			hostExecutionContextSwitcher.currentHostContext = hostExecutionContext;
			hostExecutionContextSwitcher.previousHostContext = null;
			if (HostExecutionContextManager.CheckIfHosted() && hostExecutionContext.State is IUnknownSafeHandle)
			{
				IUnknownSafeHandle unknownSafeHandle = new IUnknownSafeHandle();
				hostExecutionContextSwitcher.previousHostContext = new HostExecutionContext(unknownSafeHandle);
				IUnknownSafeHandle unknownSafeHandle2 = (IUnknownSafeHandle)hostExecutionContext.State;
				HostExecutionContextManager.SetHostSecurityContext(unknownSafeHandle2, true, unknownSafeHandle);
			}
			mutableExecutionContext.HostExecutionContext = hostExecutionContext;
			return hostExecutionContextSwitcher;
		}

		// Token: 0x06003C54 RID: 15444 RVA: 0x000E3EEC File Offset: 0x000E20EC
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public virtual void Revert(object previousState)
		{
			HostExecutionContextSwitcher hostExecutionContextSwitcher = previousState as HostExecutionContextSwitcher;
			if (hostExecutionContextSwitcher == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotOverrideSetWithoutRevert"));
			}
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			if (mutableExecutionContext != hostExecutionContextSwitcher.executionContext)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotUseSwitcherOtherThread"));
			}
			hostExecutionContextSwitcher.executionContext = null;
			HostExecutionContext hostExecutionContext = mutableExecutionContext.HostExecutionContext;
			if (hostExecutionContext != hostExecutionContextSwitcher.currentHostContext)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotUseSwitcherOtherThread"));
			}
			HostExecutionContext previousHostContext = hostExecutionContextSwitcher.previousHostContext;
			if (HostExecutionContextManager.CheckIfHosted() && previousHostContext != null && previousHostContext.State is IUnknownSafeHandle)
			{
				IUnknownSafeHandle unknownSafeHandle = (IUnknownSafeHandle)previousHostContext.State;
				HostExecutionContextManager.SetHostSecurityContext(unknownSafeHandle, false, null);
			}
			mutableExecutionContext.HostExecutionContext = previousHostContext;
		}

		// Token: 0x06003C55 RID: 15445 RVA: 0x000E3F9C File Offset: 0x000E219C
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static HostExecutionContext CaptureHostExecutionContext()
		{
			HostExecutionContext hostExecutionContext = null;
			HostExecutionContextManager currentHostExecutionContextManager = HostExecutionContextManager.GetCurrentHostExecutionContextManager();
			if (currentHostExecutionContextManager != null)
			{
				hostExecutionContext = currentHostExecutionContextManager.Capture();
			}
			return hostExecutionContext;
		}

		// Token: 0x06003C56 RID: 15446 RVA: 0x000E3FBC File Offset: 0x000E21BC
		[SecurityCritical]
		internal static object SetHostExecutionContextInternal(HostExecutionContext hostContext)
		{
			HostExecutionContextManager currentHostExecutionContextManager = HostExecutionContextManager.GetCurrentHostExecutionContextManager();
			object obj = null;
			if (currentHostExecutionContextManager != null)
			{
				obj = currentHostExecutionContextManager.SetHostExecutionContext(hostContext);
			}
			return obj;
		}

		// Token: 0x06003C57 RID: 15447 RVA: 0x000E3FE0 File Offset: 0x000E21E0
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static HostExecutionContextManager GetCurrentHostExecutionContextManager()
		{
			AppDomainManager currentAppDomainManager = AppDomainManager.CurrentAppDomainManager;
			if (currentAppDomainManager != null)
			{
				return currentAppDomainManager.HostExecutionContextManager;
			}
			return null;
		}

		// Token: 0x06003C58 RID: 15448 RVA: 0x000E3FFE File Offset: 0x000E21FE
		internal static HostExecutionContextManager GetInternalHostExecutionContextManager()
		{
			if (HostExecutionContextManager._hostExecutionContextManager == null)
			{
				HostExecutionContextManager._hostExecutionContextManager = new HostExecutionContextManager();
			}
			return HostExecutionContextManager._hostExecutionContextManager;
		}

		// Token: 0x06003C59 RID: 15449 RVA: 0x000E4016 File Offset: 0x000E2216
		public HostExecutionContextManager()
		{
		}

		// Token: 0x0400199D RID: 6557
		private static volatile bool _fIsHostedChecked;

		// Token: 0x0400199E RID: 6558
		private static volatile bool _fIsHosted;

		// Token: 0x0400199F RID: 6559
		private static HostExecutionContextManager _hostExecutionContextManager;
	}
}
