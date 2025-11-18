using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x02000526 RID: 1318
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public static class ThreadPool
	{
		// Token: 0x06003DF9 RID: 15865 RVA: 0x000E74A0 File Offset: 0x000E56A0
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
		public static bool SetMaxThreads(int workerThreads, int completionPortThreads)
		{
			return ThreadPool.SetMaxThreadsNative(workerThreads, completionPortThreads);
		}

		// Token: 0x06003DFA RID: 15866 RVA: 0x000E74A9 File Offset: 0x000E56A9
		[SecuritySafeCritical]
		public static void GetMaxThreads(out int workerThreads, out int completionPortThreads)
		{
			ThreadPool.GetMaxThreadsNative(out workerThreads, out completionPortThreads);
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x000E74B2 File Offset: 0x000E56B2
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
		public static bool SetMinThreads(int workerThreads, int completionPortThreads)
		{
			return ThreadPool.SetMinThreadsNative(workerThreads, completionPortThreads);
		}

		// Token: 0x06003DFC RID: 15868 RVA: 0x000E74BB File Offset: 0x000E56BB
		[SecuritySafeCritical]
		public static void GetMinThreads(out int workerThreads, out int completionPortThreads)
		{
			ThreadPool.GetMinThreadsNative(out workerThreads, out completionPortThreads);
		}

		// Token: 0x06003DFD RID: 15869 RVA: 0x000E74C4 File Offset: 0x000E56C4
		[SecuritySafeCritical]
		public static void GetAvailableThreads(out int workerThreads, out int completionPortThreads)
		{
			ThreadPool.GetAvailableThreadsNative(out workerThreads, out completionPortThreads);
		}

		// Token: 0x06003DFE RID: 15870 RVA: 0x000E74D0 File Offset: 0x000E56D0
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, uint millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, millisecondsTimeOutInterval, executeOnlyOnce, ref stackCrawlMark, true);
		}

		// Token: 0x06003DFF RID: 15871 RVA: 0x000E74F0 File Offset: 0x000E56F0
		[SecurityCritical]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, uint millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, millisecondsTimeOutInterval, executeOnlyOnce, ref stackCrawlMark, false);
		}

		// Token: 0x06003E00 RID: 15872 RVA: 0x000E7510 File Offset: 0x000E5710
		[SecurityCritical]
		private static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, uint millisecondsTimeOutInterval, bool executeOnlyOnce, ref StackCrawlMark stackMark, bool compressStack)
		{
			if (RemotingServices.IsTransparentProxy(waitObject))
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WaitOnTransparentProxy"));
			}
			RegisteredWaitHandle registeredWaitHandle = new RegisteredWaitHandle();
			if (callBack != null)
			{
				_ThreadPoolWaitOrTimerCallback threadPoolWaitOrTimerCallback = new _ThreadPoolWaitOrTimerCallback(callBack, state, compressStack, ref stackMark);
				state = threadPoolWaitOrTimerCallback;
				registeredWaitHandle.SetWaitObject(waitObject);
				IntPtr intPtr = ThreadPool.RegisterWaitForSingleObjectNative(waitObject, state, millisecondsTimeOutInterval, executeOnlyOnce, registeredWaitHandle, ref stackMark, compressStack);
				registeredWaitHandle.SetHandle(intPtr);
				return registeredWaitHandle;
			}
			throw new ArgumentNullException("WaitOrTimerCallback");
		}

		// Token: 0x06003E01 RID: 15873 RVA: 0x000E757C File Offset: 0x000E577C
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, int millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			if (millisecondsTimeOutInterval < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeOutInterval", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint)millisecondsTimeOutInterval, executeOnlyOnce, ref stackCrawlMark, true);
		}

		// Token: 0x06003E02 RID: 15874 RVA: 0x000E75B4 File Offset: 0x000E57B4
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, int millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			if (millisecondsTimeOutInterval < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeOutInterval", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint)millisecondsTimeOutInterval, executeOnlyOnce, ref stackCrawlMark, false);
		}

		// Token: 0x06003E03 RID: 15875 RVA: 0x000E75EC File Offset: 0x000E57EC
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, long millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			if (millisecondsTimeOutInterval < -1L)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeOutInterval", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint)millisecondsTimeOutInterval, executeOnlyOnce, ref stackCrawlMark, true);
		}

		// Token: 0x06003E04 RID: 15876 RVA: 0x000E7624 File Offset: 0x000E5824
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, long millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			if (millisecondsTimeOutInterval < -1L)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeOutInterval", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint)millisecondsTimeOutInterval, executeOnlyOnce, ref stackCrawlMark, false);
		}

		// Token: 0x06003E05 RID: 15877 RVA: 0x000E765C File Offset: 0x000E585C
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, TimeSpan timeout, bool executeOnlyOnce)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_LessEqualToIntegerMaxVal"));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint)num, executeOnlyOnce, ref stackCrawlMark, true);
		}

		// Token: 0x06003E06 RID: 15878 RVA: 0x000E76BC File Offset: 0x000E58BC
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, TimeSpan timeout, bool executeOnlyOnce)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_LessEqualToIntegerMaxVal"));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint)num, executeOnlyOnce, ref stackCrawlMark, false);
		}

		// Token: 0x06003E07 RID: 15879 RVA: 0x000E771C File Offset: 0x000E591C
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool QueueUserWorkItem(WaitCallback callBack, object state)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.QueueUserWorkItemHelper(callBack, state, ref stackCrawlMark, true);
		}

		// Token: 0x06003E08 RID: 15880 RVA: 0x000E7738 File Offset: 0x000E5938
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool QueueUserWorkItem(WaitCallback callBack)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.QueueUserWorkItemHelper(callBack, null, ref stackCrawlMark, true);
		}

		// Token: 0x06003E09 RID: 15881 RVA: 0x000E7754 File Offset: 0x000E5954
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool UnsafeQueueUserWorkItem(WaitCallback callBack, object state)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.QueueUserWorkItemHelper(callBack, state, ref stackCrawlMark, false);
		}

		// Token: 0x06003E0A RID: 15882 RVA: 0x000E7770 File Offset: 0x000E5970
		[SecurityCritical]
		private static bool QueueUserWorkItemHelper(WaitCallback callBack, object state, ref StackCrawlMark stackMark, bool compressStack)
		{
			bool flag = true;
			if (callBack != null)
			{
				ThreadPool.EnsureVMInitialized();
				try
				{
					return flag;
				}
				finally
				{
					QueueUserWorkItemCallback queueUserWorkItemCallback = new QueueUserWorkItemCallback(callBack, state, compressStack, ref stackMark);
					ThreadPoolGlobals.workQueue.Enqueue(queueUserWorkItemCallback, true);
					flag = true;
				}
			}
			throw new ArgumentNullException("WaitCallback");
		}

		// Token: 0x06003E0B RID: 15883 RVA: 0x000E77C0 File Offset: 0x000E59C0
		[SecurityCritical]
		internal static void UnsafeQueueCustomWorkItem(IThreadPoolWorkItem workItem, bool forceGlobal)
		{
			ThreadPool.EnsureVMInitialized();
			try
			{
			}
			finally
			{
				ThreadPoolGlobals.workQueue.Enqueue(workItem, forceGlobal);
			}
		}

		// Token: 0x06003E0C RID: 15884 RVA: 0x000E77F4 File Offset: 0x000E59F4
		[SecurityCritical]
		internal static bool TryPopCustomWorkItem(IThreadPoolWorkItem workItem)
		{
			return ThreadPoolGlobals.vmTpInitialized && ThreadPoolGlobals.workQueue.LocalFindAndPop(workItem);
		}

		// Token: 0x06003E0D RID: 15885 RVA: 0x000E780C File Offset: 0x000E5A0C
		[SecurityCritical]
		internal static IEnumerable<IThreadPoolWorkItem> GetQueuedWorkItems()
		{
			return ThreadPool.EnumerateQueuedWorkItems(ThreadPoolWorkQueue.allThreadQueues.Current, ThreadPoolGlobals.workQueue.queueTail);
		}

		// Token: 0x06003E0E RID: 15886 RVA: 0x000E7829 File Offset: 0x000E5A29
		internal static IEnumerable<IThreadPoolWorkItem> EnumerateQueuedWorkItems(ThreadPoolWorkQueue.WorkStealingQueue[] wsQueues, ThreadPoolWorkQueue.QueueSegment globalQueueTail)
		{
			if (wsQueues != null)
			{
				foreach (ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue in wsQueues)
				{
					if (workStealingQueue != null && workStealingQueue.m_array != null)
					{
						IThreadPoolWorkItem[] items = workStealingQueue.m_array;
						int num;
						for (int i = 0; i < items.Length; i = num + 1)
						{
							IThreadPoolWorkItem threadPoolWorkItem = items[i];
							if (threadPoolWorkItem != null)
							{
								yield return threadPoolWorkItem;
							}
							num = i;
						}
						items = null;
					}
				}
				ThreadPoolWorkQueue.WorkStealingQueue[] array = null;
			}
			if (globalQueueTail != null)
			{
				ThreadPoolWorkQueue.QueueSegment segment;
				for (segment = globalQueueTail; segment != null; segment = segment.Next)
				{
					IThreadPoolWorkItem[] items = segment.nodes;
					int num;
					for (int j = 0; j < items.Length; j = num + 1)
					{
						IThreadPoolWorkItem threadPoolWorkItem2 = items[j];
						if (threadPoolWorkItem2 != null)
						{
							yield return threadPoolWorkItem2;
						}
						num = j;
					}
					items = null;
				}
				segment = null;
			}
			yield break;
		}

		// Token: 0x06003E0F RID: 15887 RVA: 0x000E7840 File Offset: 0x000E5A40
		[SecurityCritical]
		internal static IEnumerable<IThreadPoolWorkItem> GetLocallyQueuedWorkItems()
		{
			return ThreadPool.EnumerateQueuedWorkItems(new ThreadPoolWorkQueue.WorkStealingQueue[] { ThreadPoolWorkQueueThreadLocals.threadLocals.workStealingQueue }, null);
		}

		// Token: 0x06003E10 RID: 15888 RVA: 0x000E785B File Offset: 0x000E5A5B
		[SecurityCritical]
		internal static IEnumerable<IThreadPoolWorkItem> GetGloballyQueuedWorkItems()
		{
			return ThreadPool.EnumerateQueuedWorkItems(null, ThreadPoolGlobals.workQueue.queueTail);
		}

		// Token: 0x06003E11 RID: 15889 RVA: 0x000E7870 File Offset: 0x000E5A70
		private static object[] ToObjectArray(IEnumerable<IThreadPoolWorkItem> workitems)
		{
			int num = 0;
			foreach (IThreadPoolWorkItem threadPoolWorkItem in workitems)
			{
				num++;
			}
			object[] array = new object[num];
			num = 0;
			foreach (IThreadPoolWorkItem threadPoolWorkItem2 in workitems)
			{
				if (num < array.Length)
				{
					array[num] = threadPoolWorkItem2;
				}
				num++;
			}
			return array;
		}

		// Token: 0x06003E12 RID: 15890 RVA: 0x000E7908 File Offset: 0x000E5B08
		[SecurityCritical]
		internal static object[] GetQueuedWorkItemsForDebugger()
		{
			return ThreadPool.ToObjectArray(ThreadPool.GetQueuedWorkItems());
		}

		// Token: 0x06003E13 RID: 15891 RVA: 0x000E7914 File Offset: 0x000E5B14
		[SecurityCritical]
		internal static object[] GetGloballyQueuedWorkItemsForDebugger()
		{
			return ThreadPool.ToObjectArray(ThreadPool.GetGloballyQueuedWorkItems());
		}

		// Token: 0x06003E14 RID: 15892 RVA: 0x000E7920 File Offset: 0x000E5B20
		[SecurityCritical]
		internal static object[] GetLocallyQueuedWorkItemsForDebugger()
		{
			return ThreadPool.ToObjectArray(ThreadPool.GetLocallyQueuedWorkItems());
		}

		// Token: 0x06003E15 RID: 15893
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern bool RequestWorkerThread();

		// Token: 0x06003E16 RID: 15894
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool PostQueuedCompletionStatus(NativeOverlapped* overlapped);

		// Token: 0x06003E17 RID: 15895 RVA: 0x000E792C File Offset: 0x000E5B2C
		[SecurityCritical]
		[CLSCompliant(false)]
		public unsafe static bool UnsafeQueueNativeOverlapped(NativeOverlapped* overlapped)
		{
			return ThreadPool.PostQueuedCompletionStatus(overlapped);
		}

		// Token: 0x06003E18 RID: 15896 RVA: 0x000E7934 File Offset: 0x000E5B34
		[SecurityCritical]
		private static void EnsureVMInitialized()
		{
			if (!ThreadPoolGlobals.vmTpInitialized)
			{
				ThreadPool.InitializeVMTp(ref ThreadPoolGlobals.enableWorkerTracking);
				ThreadPoolGlobals.vmTpInitialized = true;
			}
		}

		// Token: 0x06003E19 RID: 15897
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetMinThreadsNative(int workerThreads, int completionPortThreads);

		// Token: 0x06003E1A RID: 15898
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetMaxThreadsNative(int workerThreads, int completionPortThreads);

		// Token: 0x06003E1B RID: 15899
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMinThreadsNative(out int workerThreads, out int completionPortThreads);

		// Token: 0x06003E1C RID: 15900
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMaxThreadsNative(out int workerThreads, out int completionPortThreads);

		// Token: 0x06003E1D RID: 15901
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAvailableThreadsNative(out int workerThreads, out int completionPortThreads);

		// Token: 0x06003E1E RID: 15902
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool NotifyWorkItemComplete();

		// Token: 0x06003E1F RID: 15903
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ReportThreadStatus(bool isWorking);

		// Token: 0x06003E20 RID: 15904 RVA: 0x000E7951 File Offset: 0x000E5B51
		[SecuritySafeCritical]
		internal static void NotifyWorkItemProgress()
		{
			if (!ThreadPoolGlobals.vmTpInitialized)
			{
				ThreadPool.InitializeVMTp(ref ThreadPoolGlobals.enableWorkerTracking);
			}
			ThreadPool.NotifyWorkItemProgressNative();
		}

		// Token: 0x06003E21 RID: 15905
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void NotifyWorkItemProgressNative();

		// Token: 0x06003E22 RID: 15906
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsThreadPoolHosted();

		// Token: 0x06003E23 RID: 15907
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void InitializeVMTp(ref bool enableWorkerTracking);

		// Token: 0x06003E24 RID: 15908
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr RegisterWaitForSingleObjectNative(WaitHandle waitHandle, object state, uint timeOutInterval, bool executeOnlyOnce, RegisteredWaitHandle registeredWaitHandle, ref StackCrawlMark stackMark, bool compressStack);

		// Token: 0x06003E25 RID: 15909 RVA: 0x000E796B File Offset: 0x000E5B6B
		[SecuritySafeCritical]
		[Obsolete("ThreadPool.BindHandle(IntPtr) has been deprecated.  Please use ThreadPool.BindHandle(SafeHandle) instead.", false)]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static bool BindHandle(IntPtr osHandle)
		{
			return ThreadPool.BindIOCompletionCallbackNative(osHandle);
		}

		// Token: 0x06003E26 RID: 15910 RVA: 0x000E7974 File Offset: 0x000E5B74
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static bool BindHandle(SafeHandle osHandle)
		{
			if (osHandle == null)
			{
				throw new ArgumentNullException("osHandle");
			}
			bool flag = false;
			bool flag2 = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				osHandle.DangerousAddRef(ref flag2);
				flag = ThreadPool.BindIOCompletionCallbackNative(osHandle.DangerousGetHandle());
			}
			finally
			{
				if (flag2)
				{
					osHandle.DangerousRelease();
				}
			}
			return flag;
		}

		// Token: 0x06003E27 RID: 15911
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool BindIOCompletionCallbackNative(IntPtr fileHandle);

		// Token: 0x02000BFA RID: 3066
		[CompilerGenerated]
		private sealed class <EnumerateQueuedWorkItems>d__21 : IEnumerable<IThreadPoolWorkItem>, IEnumerable, IEnumerator<IThreadPoolWorkItem>, IDisposable, IEnumerator
		{
			// Token: 0x06006F82 RID: 28546 RVA: 0x001802C3 File Offset: 0x0017E4C3
			[DebuggerHidden]
			public <EnumerateQueuedWorkItems>d__21(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06006F83 RID: 28547 RVA: 0x001802DD File Offset: 0x0017E4DD
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06006F84 RID: 28548 RVA: 0x001802E0 File Offset: 0x0017E4E0
			bool IEnumerator.MoveNext()
			{
				switch (this.<>1__state)
				{
				case 0:
					this.<>1__state = -1;
					if (wsQueues != null)
					{
						array = wsQueues;
						j = 0;
						goto IL_D4;
					}
					goto IL_EE;
				case 1:
					this.<>1__state = -1;
					break;
				case 2:
					this.<>1__state = -1;
					goto IL_14C;
				default:
					return false;
				}
				IL_9F:
				int num = i;
				i = num + 1;
				IL_AF:
				if (i >= items.Length)
				{
					items = null;
				}
				else
				{
					IThreadPoolWorkItem threadPoolWorkItem = items[i];
					if (threadPoolWorkItem != null)
					{
						this.<>2__current = threadPoolWorkItem;
						this.<>1__state = 1;
						return true;
					}
					goto IL_9F;
				}
				IL_C6:
				j++;
				IL_D4:
				if (j >= array.Length)
				{
					array = null;
				}
				else
				{
					ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue = array[j];
					if (workStealingQueue != null && workStealingQueue.m_array != null)
					{
						items = workStealingQueue.m_array;
						i = 0;
						goto IL_AF;
					}
					goto IL_C6;
				}
				IL_EE:
				if (globalQueueTail != null)
				{
					segment = globalQueueTail;
					goto IL_186;
				}
				return false;
				IL_14C:
				num = j;
				j = num + 1;
				IL_15C:
				if (j >= items.Length)
				{
					items = null;
					segment = segment.Next;
				}
				else
				{
					IThreadPoolWorkItem threadPoolWorkItem2 = items[j];
					if (threadPoolWorkItem2 != null)
					{
						this.<>2__current = threadPoolWorkItem2;
						this.<>1__state = 2;
						return true;
					}
					goto IL_14C;
				}
				IL_186:
				if (segment != null)
				{
					items = segment.nodes;
					j = 0;
					goto IL_15C;
				}
				segment = null;
				return false;
			}

			// Token: 0x17001323 RID: 4899
			// (get) Token: 0x06006F85 RID: 28549 RVA: 0x00180486 File Offset: 0x0017E686
			IThreadPoolWorkItem IEnumerator<IThreadPoolWorkItem>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06006F86 RID: 28550 RVA: 0x0018048E File Offset: 0x0017E68E
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17001324 RID: 4900
			// (get) Token: 0x06006F87 RID: 28551 RVA: 0x00180495 File Offset: 0x0017E695
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06006F88 RID: 28552 RVA: 0x001804A0 File Offset: 0x0017E6A0
			[DebuggerHidden]
			IEnumerator<IThreadPoolWorkItem> IEnumerable<IThreadPoolWorkItem>.GetEnumerator()
			{
				ThreadPool.<EnumerateQueuedWorkItems>d__21 <EnumerateQueuedWorkItems>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<EnumerateQueuedWorkItems>d__ = this;
				}
				else
				{
					<EnumerateQueuedWorkItems>d__ = new ThreadPool.<EnumerateQueuedWorkItems>d__21(0);
				}
				<EnumerateQueuedWorkItems>d__.wsQueues = wsQueues;
				<EnumerateQueuedWorkItems>d__.globalQueueTail = globalQueueTail;
				return <EnumerateQueuedWorkItems>d__;
			}

			// Token: 0x06006F89 RID: 28553 RVA: 0x001804EF File Offset: 0x0017E6EF
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Threading.IThreadPoolWorkItem>.GetEnumerator();
			}

			// Token: 0x0400363A RID: 13882
			private int <>1__state;

			// Token: 0x0400363B RID: 13883
			private IThreadPoolWorkItem <>2__current;

			// Token: 0x0400363C RID: 13884
			private int <>l__initialThreadId;

			// Token: 0x0400363D RID: 13885
			private ThreadPoolWorkQueue.WorkStealingQueue[] wsQueues;

			// Token: 0x0400363E RID: 13886
			public ThreadPoolWorkQueue.WorkStealingQueue[] <>3__wsQueues;

			// Token: 0x0400363F RID: 13887
			private ThreadPoolWorkQueue.QueueSegment globalQueueTail;

			// Token: 0x04003640 RID: 13888
			public ThreadPoolWorkQueue.QueueSegment <>3__globalQueueTail;

			// Token: 0x04003641 RID: 13889
			private ThreadPoolWorkQueue.WorkStealingQueue[] <>7__wrap1;

			// Token: 0x04003642 RID: 13890
			private int <>7__wrap2;

			// Token: 0x04003643 RID: 13891
			private IThreadPoolWorkItem[] <items>5__4;

			// Token: 0x04003644 RID: 13892
			private int <i>5__5;

			// Token: 0x04003645 RID: 13893
			private ThreadPoolWorkQueue.QueueSegment <segment>5__6;
		}
	}
}
