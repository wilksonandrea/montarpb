using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x020003E6 RID: 998
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class Debugger
	{
		// Token: 0x060032FF RID: 13055 RVA: 0x000C48F9 File Offset: 0x000C2AF9
		[Obsolete("Do not create instances of the Debugger class.  Call the static methods directly on this type instead", true)]
		public Debugger()
		{
		}

		// Token: 0x06003300 RID: 13056 RVA: 0x000C4904 File Offset: 0x000C2B04
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static void Break()
		{
			if (!Debugger.IsAttached)
			{
				try
				{
					new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
				}
				catch (SecurityException)
				{
					return;
				}
			}
			Debugger.BreakInternal();
		}

		// Token: 0x06003301 RID: 13057 RVA: 0x000C4940 File Offset: 0x000C2B40
		[SecuritySafeCritical]
		private static void BreakCanThrow()
		{
			if (!Debugger.IsAttached)
			{
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			}
			Debugger.BreakInternal();
		}

		// Token: 0x06003302 RID: 13058
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BreakInternal();

		// Token: 0x06003303 RID: 13059 RVA: 0x000C495C File Offset: 0x000C2B5C
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static bool Launch()
		{
			if (Debugger.IsAttached)
			{
				return true;
			}
			try
			{
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			}
			catch (SecurityException)
			{
				return false;
			}
			return Debugger.LaunchInternal();
		}

		// Token: 0x06003304 RID: 13060 RVA: 0x000C499C File Offset: 0x000C2B9C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void NotifyOfCrossThreadDependencySlow()
		{
			Debugger.CrossThreadDependencyNotification crossThreadDependencyNotification = new Debugger.CrossThreadDependencyNotification();
			Debugger.CustomNotification(crossThreadDependencyNotification);
			if (Debugger.s_triggerThreadAbortExceptionForDebugger)
			{
				throw new ThreadAbortException();
			}
		}

		// Token: 0x06003305 RID: 13061 RVA: 0x000C49C2 File Offset: 0x000C2BC2
		[ComVisible(false)]
		public static void NotifyOfCrossThreadDependency()
		{
			if (Debugger.IsAttached)
			{
				Debugger.NotifyOfCrossThreadDependencySlow();
			}
		}

		// Token: 0x06003306 RID: 13062
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool LaunchInternal();

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06003307 RID: 13063
		[__DynamicallyInvokable]
		public static extern bool IsAttached
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06003308 RID: 13064
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Log(int level, string category, string message);

		// Token: 0x06003309 RID: 13065
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsLogging();

		// Token: 0x0600330A RID: 13066
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CustomNotification(ICustomDebuggerNotification data);

		// Token: 0x040016A1 RID: 5793
		private static bool s_triggerThreadAbortExceptionForDebugger;

		// Token: 0x040016A2 RID: 5794
		public static readonly string DefaultCategory;

		// Token: 0x02000B83 RID: 2947
		private class CrossThreadDependencyNotification : ICustomDebuggerNotification
		{
			// Token: 0x06006C63 RID: 27747 RVA: 0x00177289 File Offset: 0x00175489
			public CrossThreadDependencyNotification()
			{
			}
		}
	}
}
