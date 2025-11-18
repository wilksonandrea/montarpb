using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x020004EE RID: 1262
	[__DynamicallyInvokable]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)]
	public class SynchronizationContext
	{
		// Token: 0x06003B95 RID: 15253 RVA: 0x000E234B File Offset: 0x000E054B
		[__DynamicallyInvokable]
		public SynchronizationContext()
		{
		}

		// Token: 0x06003B96 RID: 15254 RVA: 0x000E2354 File Offset: 0x000E0554
		[SecuritySafeCritical]
		protected void SetWaitNotificationRequired()
		{
			Type type = base.GetType();
			if (SynchronizationContext.s_cachedPreparedType1 != type && SynchronizationContext.s_cachedPreparedType2 != type && SynchronizationContext.s_cachedPreparedType3 != type && SynchronizationContext.s_cachedPreparedType4 != type && SynchronizationContext.s_cachedPreparedType5 != type)
			{
				RuntimeHelpers.PrepareDelegate(new SynchronizationContext.WaitDelegate(this.Wait));
				if (SynchronizationContext.s_cachedPreparedType1 == null)
				{
					SynchronizationContext.s_cachedPreparedType1 = type;
				}
				else if (SynchronizationContext.s_cachedPreparedType2 == null)
				{
					SynchronizationContext.s_cachedPreparedType2 = type;
				}
				else if (SynchronizationContext.s_cachedPreparedType3 == null)
				{
					SynchronizationContext.s_cachedPreparedType3 = type;
				}
				else if (SynchronizationContext.s_cachedPreparedType4 == null)
				{
					SynchronizationContext.s_cachedPreparedType4 = type;
				}
				else if (SynchronizationContext.s_cachedPreparedType5 == null)
				{
					SynchronizationContext.s_cachedPreparedType5 = type;
				}
			}
			this._props |= SynchronizationContextProperties.RequireWaitNotification;
		}

		// Token: 0x06003B97 RID: 15255 RVA: 0x000E243C File Offset: 0x000E063C
		public bool IsWaitNotificationRequired()
		{
			return (this._props & SynchronizationContextProperties.RequireWaitNotification) > SynchronizationContextProperties.None;
		}

		// Token: 0x06003B98 RID: 15256 RVA: 0x000E2449 File Offset: 0x000E0649
		[__DynamicallyInvokable]
		public virtual void Send(SendOrPostCallback d, object state)
		{
			d(state);
		}

		// Token: 0x06003B99 RID: 15257 RVA: 0x000E2452 File Offset: 0x000E0652
		[__DynamicallyInvokable]
		public virtual void Post(SendOrPostCallback d, object state)
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(d.Invoke), state);
		}

		// Token: 0x06003B9A RID: 15258 RVA: 0x000E2467 File Offset: 0x000E0667
		[__DynamicallyInvokable]
		public virtual void OperationStarted()
		{
		}

		// Token: 0x06003B9B RID: 15259 RVA: 0x000E2469 File Offset: 0x000E0669
		[__DynamicallyInvokable]
		public virtual void OperationCompleted()
		{
		}

		// Token: 0x06003B9C RID: 15260 RVA: 0x000E246B File Offset: 0x000E066B
		[SecurityCritical]
		[CLSCompliant(false)]
		[PrePrepareMethod]
		public virtual int Wait(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout)
		{
			if (waitHandles == null)
			{
				throw new ArgumentNullException("waitHandles");
			}
			return SynchronizationContext.WaitHelper(waitHandles, waitAll, millisecondsTimeout);
		}

		// Token: 0x06003B9D RID: 15261
		[SecurityCritical]
		[CLSCompliant(false)]
		[PrePrepareMethod]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		protected static extern int WaitHelper(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout);

		// Token: 0x06003B9E RID: 15262 RVA: 0x000E2484 File Offset: 0x000E0684
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static void SetSynchronizationContext(SynchronizationContext syncContext)
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			mutableExecutionContext.SynchronizationContext = syncContext;
			mutableExecutionContext.SynchronizationContextNoFlow = syncContext;
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06003B9F RID: 15263 RVA: 0x000E24AC File Offset: 0x000E06AC
		[__DynamicallyInvokable]
		public static SynchronizationContext Current
		{
			[__DynamicallyInvokable]
			get
			{
				return Thread.CurrentThread.GetExecutionContextReader().SynchronizationContext ?? SynchronizationContext.GetThreadLocalContext();
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06003BA0 RID: 15264 RVA: 0x000E24D4 File Offset: 0x000E06D4
		internal static SynchronizationContext CurrentNoFlow
		{
			[FriendAccessAllowed]
			get
			{
				return Thread.CurrentThread.GetExecutionContextReader().SynchronizationContextNoFlow ?? SynchronizationContext.GetThreadLocalContext();
			}
		}

		// Token: 0x06003BA1 RID: 15265 RVA: 0x000E24FC File Offset: 0x000E06FC
		private static SynchronizationContext GetThreadLocalContext()
		{
			SynchronizationContext synchronizationContext = null;
			if (synchronizationContext == null && Environment.IsWinRTSupported)
			{
				synchronizationContext = SynchronizationContext.GetWinRTContext();
			}
			return synchronizationContext;
		}

		// Token: 0x06003BA2 RID: 15266 RVA: 0x000E251C File Offset: 0x000E071C
		[SecuritySafeCritical]
		private static SynchronizationContext GetWinRTContext()
		{
			if (!AppDomain.IsAppXModel())
			{
				return null;
			}
			object winRTDispatcherForCurrentThread = SynchronizationContext.GetWinRTDispatcherForCurrentThread();
			if (winRTDispatcherForCurrentThread != null)
			{
				return SynchronizationContext.GetWinRTSynchronizationContextFactory().Create(winRTDispatcherForCurrentThread);
			}
			return null;
		}

		// Token: 0x06003BA3 RID: 15267 RVA: 0x000E2548 File Offset: 0x000E0748
		[SecurityCritical]
		private static WinRTSynchronizationContextFactoryBase GetWinRTSynchronizationContextFactory()
		{
			WinRTSynchronizationContextFactoryBase winRTSynchronizationContextFactoryBase = SynchronizationContext.s_winRTContextFactory;
			if (winRTSynchronizationContextFactoryBase == null)
			{
				Type type = Type.GetType("System.Threading.WinRTSynchronizationContextFactory, System.Runtime.WindowsRuntime, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", true);
				winRTSynchronizationContextFactoryBase = (SynchronizationContext.s_winRTContextFactory = (WinRTSynchronizationContextFactoryBase)Activator.CreateInstance(type, true));
			}
			return winRTSynchronizationContextFactoryBase;
		}

		// Token: 0x06003BA4 RID: 15268
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Interface)]
		private static extern object GetWinRTDispatcherForCurrentThread();

		// Token: 0x06003BA5 RID: 15269 RVA: 0x000E257E File Offset: 0x000E077E
		[__DynamicallyInvokable]
		public virtual SynchronizationContext CreateCopy()
		{
			return new SynchronizationContext();
		}

		// Token: 0x06003BA6 RID: 15270 RVA: 0x000E2585 File Offset: 0x000E0785
		[SecurityCritical]
		private static int InvokeWaitMethodHelper(SynchronizationContext syncContext, IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout)
		{
			return syncContext.Wait(waitHandles, waitAll, millisecondsTimeout);
		}

		// Token: 0x0400196F RID: 6511
		private SynchronizationContextProperties _props;

		// Token: 0x04001970 RID: 6512
		private static Type s_cachedPreparedType1;

		// Token: 0x04001971 RID: 6513
		private static Type s_cachedPreparedType2;

		// Token: 0x04001972 RID: 6514
		private static Type s_cachedPreparedType3;

		// Token: 0x04001973 RID: 6515
		private static Type s_cachedPreparedType4;

		// Token: 0x04001974 RID: 6516
		private static Type s_cachedPreparedType5;

		// Token: 0x04001975 RID: 6517
		[SecurityCritical]
		private static WinRTSynchronizationContextFactoryBase s_winRTContextFactory;

		// Token: 0x02000BEE RID: 3054
		// (Invoke) Token: 0x06006F58 RID: 28504
		private delegate int WaitDelegate(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout);
	}
}
