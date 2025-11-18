using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x02000501 RID: 1281
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public static class Monitor
	{
		// Token: 0x06003C64 RID: 15460
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Enter(object obj);

		// Token: 0x06003C65 RID: 15461 RVA: 0x000E40D4 File Offset: 0x000E22D4
		[__DynamicallyInvokable]
		public static void Enter(object obj, ref bool lockTaken)
		{
			if (lockTaken)
			{
				Monitor.ThrowLockTakenException();
			}
			Monitor.ReliableEnter(obj, ref lockTaken);
		}

		// Token: 0x06003C66 RID: 15462 RVA: 0x000E40E6 File Offset: 0x000E22E6
		private static void ThrowLockTakenException()
		{
			throw new ArgumentException(Environment.GetResourceString("Argument_MustBeFalse"), "lockTaken");
		}

		// Token: 0x06003C67 RID: 15463
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReliableEnter(object obj, ref bool lockTaken);

		// Token: 0x06003C68 RID: 15464
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Exit(object obj);

		// Token: 0x06003C69 RID: 15465 RVA: 0x000E40FC File Offset: 0x000E22FC
		[__DynamicallyInvokable]
		public static bool TryEnter(object obj)
		{
			bool flag = false;
			Monitor.TryEnter(obj, 0, ref flag);
			return flag;
		}

		// Token: 0x06003C6A RID: 15466 RVA: 0x000E4115 File Offset: 0x000E2315
		[__DynamicallyInvokable]
		public static void TryEnter(object obj, ref bool lockTaken)
		{
			if (lockTaken)
			{
				Monitor.ThrowLockTakenException();
			}
			Monitor.ReliableEnterTimeout(obj, 0, ref lockTaken);
		}

		// Token: 0x06003C6B RID: 15467 RVA: 0x000E4128 File Offset: 0x000E2328
		[__DynamicallyInvokable]
		public static bool TryEnter(object obj, int millisecondsTimeout)
		{
			bool flag = false;
			Monitor.TryEnter(obj, millisecondsTimeout, ref flag);
			return flag;
		}

		// Token: 0x06003C6C RID: 15468 RVA: 0x000E4144 File Offset: 0x000E2344
		private static int MillisecondsTimeoutFromTimeSpan(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			return (int)num;
		}

		// Token: 0x06003C6D RID: 15469 RVA: 0x000E417F File Offset: 0x000E237F
		[__DynamicallyInvokable]
		public static bool TryEnter(object obj, TimeSpan timeout)
		{
			return Monitor.TryEnter(obj, Monitor.MillisecondsTimeoutFromTimeSpan(timeout));
		}

		// Token: 0x06003C6E RID: 15470 RVA: 0x000E418D File Offset: 0x000E238D
		[__DynamicallyInvokable]
		public static void TryEnter(object obj, int millisecondsTimeout, ref bool lockTaken)
		{
			if (lockTaken)
			{
				Monitor.ThrowLockTakenException();
			}
			Monitor.ReliableEnterTimeout(obj, millisecondsTimeout, ref lockTaken);
		}

		// Token: 0x06003C6F RID: 15471 RVA: 0x000E41A0 File Offset: 0x000E23A0
		[__DynamicallyInvokable]
		public static void TryEnter(object obj, TimeSpan timeout, ref bool lockTaken)
		{
			if (lockTaken)
			{
				Monitor.ThrowLockTakenException();
			}
			Monitor.ReliableEnterTimeout(obj, Monitor.MillisecondsTimeoutFromTimeSpan(timeout), ref lockTaken);
		}

		// Token: 0x06003C70 RID: 15472
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReliableEnterTimeout(object obj, int timeout, ref bool lockTaken);

		// Token: 0x06003C71 RID: 15473 RVA: 0x000E41B8 File Offset: 0x000E23B8
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static bool IsEntered(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			return Monitor.IsEnteredNative(obj);
		}

		// Token: 0x06003C72 RID: 15474
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsEnteredNative(object obj);

		// Token: 0x06003C73 RID: 15475
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ObjWait(bool exitContext, int millisecondsTimeout, object obj);

		// Token: 0x06003C74 RID: 15476 RVA: 0x000E41CE File Offset: 0x000E23CE
		[SecuritySafeCritical]
		public static bool Wait(object obj, int millisecondsTimeout, bool exitContext)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			return Monitor.ObjWait(exitContext, millisecondsTimeout, obj);
		}

		// Token: 0x06003C75 RID: 15477 RVA: 0x000E41E6 File Offset: 0x000E23E6
		public static bool Wait(object obj, TimeSpan timeout, bool exitContext)
		{
			return Monitor.Wait(obj, Monitor.MillisecondsTimeoutFromTimeSpan(timeout), exitContext);
		}

		// Token: 0x06003C76 RID: 15478 RVA: 0x000E41F5 File Offset: 0x000E23F5
		[__DynamicallyInvokable]
		public static bool Wait(object obj, int millisecondsTimeout)
		{
			return Monitor.Wait(obj, millisecondsTimeout, false);
		}

		// Token: 0x06003C77 RID: 15479 RVA: 0x000E41FF File Offset: 0x000E23FF
		[__DynamicallyInvokable]
		public static bool Wait(object obj, TimeSpan timeout)
		{
			return Monitor.Wait(obj, Monitor.MillisecondsTimeoutFromTimeSpan(timeout), false);
		}

		// Token: 0x06003C78 RID: 15480 RVA: 0x000E420E File Offset: 0x000E240E
		[__DynamicallyInvokable]
		public static bool Wait(object obj)
		{
			return Monitor.Wait(obj, -1, false);
		}

		// Token: 0x06003C79 RID: 15481
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ObjPulse(object obj);

		// Token: 0x06003C7A RID: 15482 RVA: 0x000E4218 File Offset: 0x000E2418
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static void Pulse(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			Monitor.ObjPulse(obj);
		}

		// Token: 0x06003C7B RID: 15483
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ObjPulseAll(object obj);

		// Token: 0x06003C7C RID: 15484 RVA: 0x000E422E File Offset: 0x000E242E
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static void PulseAll(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			Monitor.ObjPulseAll(obj);
		}
	}
}
