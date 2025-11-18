using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Threading
{
	// Token: 0x02000516 RID: 1302
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_Thread))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class Thread : CriticalFinalizerObject, _Thread
	{
		// Token: 0x06003D26 RID: 15654 RVA: 0x000E6015 File Offset: 0x000E4215
		private static void AsyncLocalSetCurrentCulture(AsyncLocalValueChangedArgs<CultureInfo> args)
		{
			Thread.CurrentThread.m_CurrentCulture = args.CurrentValue;
		}

		// Token: 0x06003D27 RID: 15655 RVA: 0x000E6028 File Offset: 0x000E4228
		private static void AsyncLocalSetCurrentUICulture(AsyncLocalValueChangedArgs<CultureInfo> args)
		{
			Thread.CurrentThread.m_CurrentUICulture = args.CurrentValue;
		}

		// Token: 0x06003D28 RID: 15656 RVA: 0x000E603B File Offset: 0x000E423B
		[SecuritySafeCritical]
		public Thread(ThreadStart start)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			this.SetStartHelper(start, 0);
		}

		// Token: 0x06003D29 RID: 15657 RVA: 0x000E6059 File Offset: 0x000E4259
		[SecuritySafeCritical]
		public Thread(ThreadStart start, int maxStackSize)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			if (0 > maxStackSize)
			{
				throw new ArgumentOutOfRangeException("maxStackSize", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.SetStartHelper(start, maxStackSize);
		}

		// Token: 0x06003D2A RID: 15658 RVA: 0x000E6090 File Offset: 0x000E4290
		[SecuritySafeCritical]
		public Thread(ParameterizedThreadStart start)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			this.SetStartHelper(start, 0);
		}

		// Token: 0x06003D2B RID: 15659 RVA: 0x000E60AE File Offset: 0x000E42AE
		[SecuritySafeCritical]
		public Thread(ParameterizedThreadStart start, int maxStackSize)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			if (0 > maxStackSize)
			{
				throw new ArgumentOutOfRangeException("maxStackSize", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.SetStartHelper(start, maxStackSize);
		}

		// Token: 0x06003D2C RID: 15660 RVA: 0x000E60E5 File Offset: 0x000E42E5
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return this.m_ManagedThreadId;
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06003D2D RID: 15661
		[__DynamicallyInvokable]
		public extern int ManagedThreadId
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06003D2E RID: 15662 RVA: 0x000E60F0 File Offset: 0x000E42F0
		internal ThreadHandle GetNativeHandle()
		{
			IntPtr dont_USE_InternalThread = this.DONT_USE_InternalThread;
			if (dont_USE_InternalThread.IsNull())
			{
				throw new ArgumentException(null, Environment.GetResourceString("Argument_InvalidHandle"));
			}
			return new ThreadHandle(dont_USE_InternalThread);
		}

		// Token: 0x06003D2F RID: 15663 RVA: 0x000E6124 File Offset: 0x000E4324
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Start()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.Start(ref stackCrawlMark);
		}

		// Token: 0x06003D30 RID: 15664 RVA: 0x000E613C File Offset: 0x000E433C
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Start(object parameter)
		{
			if (this.m_Delegate is ThreadStart)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ThreadWrongThreadStart"));
			}
			this.m_ThreadStartArg = parameter;
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.Start(ref stackCrawlMark);
		}

		// Token: 0x06003D31 RID: 15665 RVA: 0x000E6178 File Offset: 0x000E4378
		[SecuritySafeCritical]
		private void Start(ref StackCrawlMark stackMark)
		{
			this.StartupSetApartmentStateInternal();
			if (this.m_Delegate != null)
			{
				ThreadHelper threadHelper = (ThreadHelper)this.m_Delegate.Target;
				ExecutionContext executionContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx);
				threadHelper.SetExecutionContextHelper(executionContext);
			}
			IPrincipal principal = CallContext.Principal;
			this.StartInternal(principal, ref stackMark);
		}

		// Token: 0x06003D32 RID: 15666 RVA: 0x000E61C1 File Offset: 0x000E43C1
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal ExecutionContext.Reader GetExecutionContextReader()
		{
			return new ExecutionContext.Reader(this.m_ExecutionContext);
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06003D33 RID: 15667 RVA: 0x000E61CE File Offset: 0x000E43CE
		// (set) Token: 0x06003D34 RID: 15668 RVA: 0x000E61D9 File Offset: 0x000E43D9
		internal bool ExecutionContextBelongsToCurrentScope
		{
			get
			{
				return !this.m_ExecutionContextBelongsToOuterScope;
			}
			set
			{
				this.m_ExecutionContextBelongsToOuterScope = !value;
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06003D35 RID: 15669 RVA: 0x000E61E8 File Offset: 0x000E43E8
		public ExecutionContext ExecutionContext
		{
			[SecuritySafeCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			get
			{
				ExecutionContext executionContext;
				if (this == Thread.CurrentThread)
				{
					executionContext = this.GetMutableExecutionContext();
				}
				else
				{
					executionContext = this.m_ExecutionContext;
				}
				return executionContext;
			}
		}

		// Token: 0x06003D36 RID: 15670 RVA: 0x000E6210 File Offset: 0x000E4410
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal ExecutionContext GetMutableExecutionContext()
		{
			if (this.m_ExecutionContext == null)
			{
				this.m_ExecutionContext = new ExecutionContext();
			}
			else if (!this.ExecutionContextBelongsToCurrentScope)
			{
				ExecutionContext executionContext = this.m_ExecutionContext.CreateMutableCopy();
				this.m_ExecutionContext = executionContext;
			}
			this.ExecutionContextBelongsToCurrentScope = true;
			return this.m_ExecutionContext;
		}

		// Token: 0x06003D37 RID: 15671 RVA: 0x000E625A File Offset: 0x000E445A
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void SetExecutionContext(ExecutionContext value, bool belongsToCurrentScope)
		{
			this.m_ExecutionContext = value;
			this.ExecutionContextBelongsToCurrentScope = belongsToCurrentScope;
		}

		// Token: 0x06003D38 RID: 15672 RVA: 0x000E626A File Offset: 0x000E446A
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void SetExecutionContext(ExecutionContext.Reader value, bool belongsToCurrentScope)
		{
			this.m_ExecutionContext = value.DangerousGetRawExecutionContext();
			this.ExecutionContextBelongsToCurrentScope = belongsToCurrentScope;
		}

		// Token: 0x06003D39 RID: 15673
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void StartInternal(IPrincipal principal, ref StackCrawlMark stackMark);

		// Token: 0x06003D3A RID: 15674 RVA: 0x000E6280 File Offset: 0x000E4480
		[SecurityCritical]
		[Obsolete("Thread.SetCompressedStack is no longer supported. Please use the System.Threading.CompressedStack class")]
		public void SetCompressedStack(CompressedStack stack)
		{
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ThreadAPIsNotSupported"));
		}

		// Token: 0x06003D3B RID: 15675
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr SetAppDomainStack(SafeCompressedStackHandle csHandle);

		// Token: 0x06003D3C RID: 15676
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void RestoreAppDomainStack(IntPtr appDomainStack);

		// Token: 0x06003D3D RID: 15677 RVA: 0x000E6291 File Offset: 0x000E4491
		[SecurityCritical]
		[Obsolete("Thread.GetCompressedStack is no longer supported. Please use the System.Threading.CompressedStack class")]
		public CompressedStack GetCompressedStack()
		{
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ThreadAPIsNotSupported"));
		}

		// Token: 0x06003D3E RID: 15678
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr InternalGetCurrentThread();

		// Token: 0x06003D3F RID: 15679 RVA: 0x000E62A2 File Offset: 0x000E44A2
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
		public void Abort(object stateInfo)
		{
			this.AbortReason = stateInfo;
			this.AbortInternal();
		}

		// Token: 0x06003D40 RID: 15680 RVA: 0x000E62B1 File Offset: 0x000E44B1
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
		public void Abort()
		{
			this.AbortInternal();
		}

		// Token: 0x06003D41 RID: 15681
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AbortInternal();

		// Token: 0x06003D42 RID: 15682 RVA: 0x000E62BC File Offset: 0x000E44BC
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
		public static void ResetAbort()
		{
			Thread currentThread = Thread.CurrentThread;
			if ((currentThread.ThreadState & ThreadState.AbortRequested) == ThreadState.Running)
			{
				throw new ThreadStateException(Environment.GetResourceString("ThreadState_NoAbortRequested"));
			}
			currentThread.ResetAbortNative();
			currentThread.ClearAbortReason();
		}

		// Token: 0x06003D43 RID: 15683
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ResetAbortNative();

		// Token: 0x06003D44 RID: 15684 RVA: 0x000E62F9 File Offset: 0x000E44F9
		[SecuritySafeCritical]
		[Obsolete("Thread.Suspend has been deprecated.  Please use other classes in System.Threading, such as Monitor, Mutex, Event, and Semaphore, to synchronize Threads or protect resources.  http://go.microsoft.com/fwlink/?linkid=14202", false)]
		[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
		[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
		public void Suspend()
		{
			this.SuspendInternal();
		}

		// Token: 0x06003D45 RID: 15685
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SuspendInternal();

		// Token: 0x06003D46 RID: 15686 RVA: 0x000E6301 File Offset: 0x000E4501
		[SecuritySafeCritical]
		[Obsolete("Thread.Resume has been deprecated.  Please use other classes in System.Threading, such as Monitor, Mutex, Event, and Semaphore, to synchronize Threads or protect resources.  http://go.microsoft.com/fwlink/?linkid=14202", false)]
		[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
		public void Resume()
		{
			this.ResumeInternal();
		}

		// Token: 0x06003D47 RID: 15687
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ResumeInternal();

		// Token: 0x06003D48 RID: 15688 RVA: 0x000E6309 File Offset: 0x000E4509
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
		public void Interrupt()
		{
			this.InterruptInternal();
		}

		// Token: 0x06003D49 RID: 15689
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InterruptInternal();

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06003D4A RID: 15690 RVA: 0x000E6311 File Offset: 0x000E4511
		// (set) Token: 0x06003D4B RID: 15691 RVA: 0x000E6319 File Offset: 0x000E4519
		public ThreadPriority Priority
		{
			[SecuritySafeCritical]
			get
			{
				return (ThreadPriority)this.GetPriorityNative();
			}
			[SecuritySafeCritical]
			[HostProtection(SecurityAction.LinkDemand, SelfAffectingThreading = true)]
			set
			{
				this.SetPriorityNative((int)value);
			}
		}

		// Token: 0x06003D4C RID: 15692
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetPriorityNative();

		// Token: 0x06003D4D RID: 15693
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPriorityNative(int priority);

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06003D4E RID: 15694
		public extern bool IsAlive
		{
			[SecuritySafeCritical]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06003D4F RID: 15695
		public extern bool IsThreadPoolThread
		{
			[SecuritySafeCritical]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06003D50 RID: 15696
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool JoinInternal(int millisecondsTimeout);

		// Token: 0x06003D51 RID: 15697 RVA: 0x000E6322 File Offset: 0x000E4522
		[SecuritySafeCritical]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		public void Join()
		{
			this.JoinInternal(-1);
		}

		// Token: 0x06003D52 RID: 15698 RVA: 0x000E632C File Offset: 0x000E452C
		[SecuritySafeCritical]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		public bool Join(int millisecondsTimeout)
		{
			return this.JoinInternal(millisecondsTimeout);
		}

		// Token: 0x06003D53 RID: 15699 RVA: 0x000E6338 File Offset: 0x000E4538
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		public bool Join(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			return this.Join((int)num);
		}

		// Token: 0x06003D54 RID: 15700
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SleepInternal(int millisecondsTimeout);

		// Token: 0x06003D55 RID: 15701 RVA: 0x000E6379 File Offset: 0x000E4579
		[SecuritySafeCritical]
		public static void Sleep(int millisecondsTimeout)
		{
			Thread.SleepInternal(millisecondsTimeout);
			if (AppDomainPauseManager.IsPaused)
			{
				AppDomainPauseManager.ResumeEvent.WaitOneWithoutFAS();
			}
		}

		// Token: 0x06003D56 RID: 15702 RVA: 0x000E6394 File Offset: 0x000E4594
		public static void Sleep(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			Thread.Sleep((int)num);
		}

		// Token: 0x06003D57 RID: 15703
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetCurrentProcessorNumber();

		// Token: 0x06003D58 RID: 15704 RVA: 0x000E63D4 File Offset: 0x000E45D4
		private static int RefreshCurrentProcessorId()
		{
			int num = Thread.GetCurrentProcessorNumber();
			if (num < 0)
			{
				num = Environment.CurrentManagedThreadId;
			}
			num += 100;
			Thread.t_currentProcessorIdCache = ((num << 16) & int.MaxValue) | 5000;
			return num;
		}

		// Token: 0x06003D59 RID: 15705 RVA: 0x000E640C File Offset: 0x000E460C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static int GetCurrentProcessorId()
		{
			int num = Thread.t_currentProcessorIdCache--;
			if ((num & 65535) == 0)
			{
				return Thread.RefreshCurrentProcessorId();
			}
			return num >> 16;
		}

		// Token: 0x06003D5A RID: 15706
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SpinWaitInternal(int iterations);

		// Token: 0x06003D5B RID: 15707 RVA: 0x000E643A File Offset: 0x000E463A
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		public static void SpinWait(int iterations)
		{
			Thread.SpinWaitInternal(iterations);
		}

		// Token: 0x06003D5C RID: 15708
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern bool YieldInternal();

		// Token: 0x06003D5D RID: 15709 RVA: 0x000E6442 File Offset: 0x000E4642
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		public static bool Yield()
		{
			return Thread.YieldInternal();
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06003D5E RID: 15710 RVA: 0x000E6449 File Offset: 0x000E4649
		[__DynamicallyInvokable]
		public static Thread CurrentThread
		{
			[SecuritySafeCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[__DynamicallyInvokable]
			get
			{
				return Thread.GetCurrentThreadNative();
			}
		}

		// Token: 0x06003D5F RID: 15711
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Thread GetCurrentThreadNative();

		// Token: 0x06003D60 RID: 15712 RVA: 0x000E6450 File Offset: 0x000E4650
		[SecurityCritical]
		private void SetStartHelper(Delegate start, int maxStackSize)
		{
			ulong processDefaultStackSize = Thread.GetProcessDefaultStackSize();
			if ((ulong)maxStackSize > processDefaultStackSize)
			{
				try
				{
					CodeAccessPermission.Demand(PermissionType.FullTrust);
				}
				catch (SecurityException)
				{
					maxStackSize = (int)Math.Min(processDefaultStackSize, 2147483647UL);
				}
			}
			ThreadHelper threadHelper = new ThreadHelper(start);
			if (start is ThreadStart)
			{
				this.SetStart(new ThreadStart(threadHelper.ThreadStart), maxStackSize);
				return;
			}
			this.SetStart(new ParameterizedThreadStart(threadHelper.ThreadStart), maxStackSize);
		}

		// Token: 0x06003D61 RID: 15713
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern ulong GetProcessDefaultStackSize();

		// Token: 0x06003D62 RID: 15714
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetStart(Delegate start, int maxStackSize);

		// Token: 0x06003D63 RID: 15715 RVA: 0x000E64C8 File Offset: 0x000E46C8
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		~Thread()
		{
			this.InternalFinalize();
		}

		// Token: 0x06003D64 RID: 15716
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalFinalize();

		// Token: 0x06003D65 RID: 15717
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DisableComObjectEagerCleanup();

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06003D66 RID: 15718 RVA: 0x000E64F4 File Offset: 0x000E46F4
		// (set) Token: 0x06003D67 RID: 15719 RVA: 0x000E64FC File Offset: 0x000E46FC
		public bool IsBackground
		{
			[SecuritySafeCritical]
			get
			{
				return this.IsBackgroundNative();
			}
			[SecuritySafeCritical]
			[HostProtection(SecurityAction.LinkDemand, SelfAffectingThreading = true)]
			set
			{
				this.SetBackgroundNative(value);
			}
		}

		// Token: 0x06003D68 RID: 15720
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsBackgroundNative();

		// Token: 0x06003D69 RID: 15721
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBackgroundNative(bool isBackground);

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06003D6A RID: 15722 RVA: 0x000E6505 File Offset: 0x000E4705
		public ThreadState ThreadState
		{
			[SecuritySafeCritical]
			get
			{
				return (ThreadState)this.GetThreadStateNative();
			}
		}

		// Token: 0x06003D6B RID: 15723
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetThreadStateNative();

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06003D6C RID: 15724 RVA: 0x000E650D File Offset: 0x000E470D
		// (set) Token: 0x06003D6D RID: 15725 RVA: 0x000E6515 File Offset: 0x000E4715
		[Obsolete("The ApartmentState property has been deprecated.  Use GetApartmentState, SetApartmentState or TrySetApartmentState instead.", false)]
		public ApartmentState ApartmentState
		{
			[SecuritySafeCritical]
			get
			{
				return (ApartmentState)this.GetApartmentStateNative();
			}
			[SecuritySafeCritical]
			[HostProtection(SecurityAction.LinkDemand, Synchronization = true, SelfAffectingThreading = true)]
			set
			{
				this.SetApartmentStateNative((int)value, true);
			}
		}

		// Token: 0x06003D6E RID: 15726 RVA: 0x000E6520 File Offset: 0x000E4720
		[SecuritySafeCritical]
		public ApartmentState GetApartmentState()
		{
			return (ApartmentState)this.GetApartmentStateNative();
		}

		// Token: 0x06003D6F RID: 15727 RVA: 0x000E6528 File Offset: 0x000E4728
		[SecuritySafeCritical]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, SelfAffectingThreading = true)]
		public bool TrySetApartmentState(ApartmentState state)
		{
			return this.SetApartmentStateHelper(state, false);
		}

		// Token: 0x06003D70 RID: 15728 RVA: 0x000E6534 File Offset: 0x000E4734
		[SecuritySafeCritical]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, SelfAffectingThreading = true)]
		public void SetApartmentState(ApartmentState state)
		{
			if (!this.SetApartmentStateHelper(state, true))
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ApartmentStateSwitchFailed"));
			}
		}

		// Token: 0x06003D71 RID: 15729 RVA: 0x000E6560 File Offset: 0x000E4760
		[SecurityCritical]
		private bool SetApartmentStateHelper(ApartmentState state, bool fireMDAOnMismatch)
		{
			ApartmentState apartmentState = (ApartmentState)this.SetApartmentStateNative((int)state, fireMDAOnMismatch);
			return (state == ApartmentState.Unknown && apartmentState == ApartmentState.MTA) || apartmentState == state;
		}

		// Token: 0x06003D72 RID: 15730
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetApartmentStateNative();

		// Token: 0x06003D73 RID: 15731
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int SetApartmentStateNative(int state, bool fireMDAOnMismatch);

		// Token: 0x06003D74 RID: 15732
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void StartupSetApartmentStateInternal();

		// Token: 0x06003D75 RID: 15733 RVA: 0x000E6587 File Offset: 0x000E4787
		[HostProtection(SecurityAction.LinkDemand, SharedState = true, ExternalThreading = true)]
		public static LocalDataStoreSlot AllocateDataSlot()
		{
			return Thread.LocalDataStoreManager.AllocateDataSlot();
		}

		// Token: 0x06003D76 RID: 15734 RVA: 0x000E6593 File Offset: 0x000E4793
		[HostProtection(SecurityAction.LinkDemand, SharedState = true, ExternalThreading = true)]
		public static LocalDataStoreSlot AllocateNamedDataSlot(string name)
		{
			return Thread.LocalDataStoreManager.AllocateNamedDataSlot(name);
		}

		// Token: 0x06003D77 RID: 15735 RVA: 0x000E65A0 File Offset: 0x000E47A0
		[HostProtection(SecurityAction.LinkDemand, SharedState = true, ExternalThreading = true)]
		public static LocalDataStoreSlot GetNamedDataSlot(string name)
		{
			return Thread.LocalDataStoreManager.GetNamedDataSlot(name);
		}

		// Token: 0x06003D78 RID: 15736 RVA: 0x000E65AD File Offset: 0x000E47AD
		[HostProtection(SecurityAction.LinkDemand, SharedState = true, ExternalThreading = true)]
		public static void FreeNamedDataSlot(string name)
		{
			Thread.LocalDataStoreManager.FreeNamedDataSlot(name);
		}

		// Token: 0x06003D79 RID: 15737 RVA: 0x000E65BC File Offset: 0x000E47BC
		[HostProtection(SecurityAction.LinkDemand, SharedState = true, ExternalThreading = true)]
		public static object GetData(LocalDataStoreSlot slot)
		{
			LocalDataStoreHolder localDataStoreHolder = Thread.s_LocalDataStore;
			if (localDataStoreHolder == null)
			{
				Thread.LocalDataStoreManager.ValidateSlot(slot);
				return null;
			}
			return localDataStoreHolder.Store.GetData(slot);
		}

		// Token: 0x06003D7A RID: 15738 RVA: 0x000E65EC File Offset: 0x000E47EC
		[HostProtection(SecurityAction.LinkDemand, SharedState = true, ExternalThreading = true)]
		public static void SetData(LocalDataStoreSlot slot, object data)
		{
			LocalDataStoreHolder localDataStoreHolder = Thread.s_LocalDataStore;
			if (localDataStoreHolder == null)
			{
				localDataStoreHolder = Thread.LocalDataStoreManager.CreateLocalDataStore();
				Thread.s_LocalDataStore = localDataStoreHolder;
			}
			localDataStoreHolder.Store.SetData(slot, data);
		}

		// Token: 0x06003D7B RID: 15739
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool nativeGetSafeCulture(Thread t, int appDomainId, bool isUI, ref CultureInfo safeCulture);

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06003D7C RID: 15740 RVA: 0x000E6620 File Offset: 0x000E4820
		// (set) Token: 0x06003D7D RID: 15741 RVA: 0x000E6640 File Offset: 0x000E4840
		[__DynamicallyInvokable]
		public CultureInfo CurrentUICulture
		{
			[__DynamicallyInvokable]
			get
			{
				if (AppDomain.IsAppXModel())
				{
					return CultureInfo.GetCultureInfoForUserPreferredLanguageInAppX() ?? this.GetCurrentUICultureNoAppX();
				}
				return this.GetCurrentUICultureNoAppX();
			}
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				CultureInfo.VerifyCultureName(value, true);
				if (!Thread.nativeSetThreadUILocale(value.SortName))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidResourceCultureName", new object[] { value.Name }));
				}
				value.StartCrossDomainTracking();
				if (!AppContextSwitches.NoAsyncCurrentCulture)
				{
					if (Thread.s_asyncLocalCurrentUICulture == null)
					{
						Interlocked.CompareExchange<AsyncLocal<CultureInfo>>(ref Thread.s_asyncLocalCurrentUICulture, new AsyncLocal<CultureInfo>(new Action<AsyncLocalValueChangedArgs<CultureInfo>>(Thread.AsyncLocalSetCurrentUICulture)), null);
					}
					Thread.s_asyncLocalCurrentUICulture.Value = value;
					return;
				}
				this.m_CurrentUICulture = value;
			}
		}

		// Token: 0x06003D7E RID: 15742 RVA: 0x000E66D4 File Offset: 0x000E48D4
		[SecuritySafeCritical]
		internal CultureInfo GetCurrentUICultureNoAppX()
		{
			if (this.m_CurrentUICulture == null)
			{
				CultureInfo defaultThreadCurrentUICulture = CultureInfo.DefaultThreadCurrentUICulture;
				if (defaultThreadCurrentUICulture == null)
				{
					return CultureInfo.UserDefaultUICulture;
				}
				return defaultThreadCurrentUICulture;
			}
			else
			{
				CultureInfo cultureInfo = null;
				if (!Thread.nativeGetSafeCulture(this, Thread.GetDomainID(), true, ref cultureInfo) || cultureInfo == null)
				{
					return CultureInfo.UserDefaultUICulture;
				}
				return cultureInfo;
			}
		}

		// Token: 0x06003D7F RID: 15743
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool nativeSetThreadUILocale(string locale);

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06003D80 RID: 15744 RVA: 0x000E6716 File Offset: 0x000E4916
		// (set) Token: 0x06003D81 RID: 15745 RVA: 0x000E6738 File Offset: 0x000E4938
		[__DynamicallyInvokable]
		public CultureInfo CurrentCulture
		{
			[__DynamicallyInvokable]
			get
			{
				if (AppDomain.IsAppXModel())
				{
					return CultureInfo.GetCultureInfoForUserPreferredLanguageInAppX() ?? this.GetCurrentCultureNoAppX();
				}
				return this.GetCurrentCultureNoAppX();
			}
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				CultureInfo.nativeSetThreadLocale(value.SortName);
				value.StartCrossDomainTracking();
				if (!AppContextSwitches.NoAsyncCurrentCulture)
				{
					if (Thread.s_asyncLocalCurrentCulture == null)
					{
						Interlocked.CompareExchange<AsyncLocal<CultureInfo>>(ref Thread.s_asyncLocalCurrentCulture, new AsyncLocal<CultureInfo>(new Action<AsyncLocalValueChangedArgs<CultureInfo>>(Thread.AsyncLocalSetCurrentCulture)), null);
					}
					Thread.s_asyncLocalCurrentCulture.Value = value;
					return;
				}
				this.m_CurrentCulture = value;
			}
		}

		// Token: 0x06003D82 RID: 15746 RVA: 0x000E67A4 File Offset: 0x000E49A4
		[SecuritySafeCritical]
		private CultureInfo GetCurrentCultureNoAppX()
		{
			if (this.m_CurrentCulture == null)
			{
				CultureInfo defaultThreadCurrentCulture = CultureInfo.DefaultThreadCurrentCulture;
				if (defaultThreadCurrentCulture == null)
				{
					return CultureInfo.UserDefaultCulture;
				}
				return defaultThreadCurrentCulture;
			}
			else
			{
				CultureInfo cultureInfo = null;
				if (!Thread.nativeGetSafeCulture(this, Thread.GetDomainID(), false, ref cultureInfo) || cultureInfo == null)
				{
					return CultureInfo.UserDefaultCulture;
				}
				return cultureInfo;
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06003D83 RID: 15747 RVA: 0x000E67E6 File Offset: 0x000E49E6
		public static Context CurrentContext
		{
			[SecurityCritical]
			get
			{
				return Thread.CurrentThread.GetCurrentContextInternal();
			}
		}

		// Token: 0x06003D84 RID: 15748 RVA: 0x000E67F2 File Offset: 0x000E49F2
		[SecurityCritical]
		internal Context GetCurrentContextInternal()
		{
			if (this.m_Context == null)
			{
				this.m_Context = Context.DefaultContext;
			}
			return this.m_Context;
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06003D85 RID: 15749 RVA: 0x000E6810 File Offset: 0x000E4A10
		// (set) Token: 0x06003D86 RID: 15750 RVA: 0x000E6868 File Offset: 0x000E4A68
		public static IPrincipal CurrentPrincipal
		{
			[SecuritySafeCritical]
			get
			{
				Thread currentThread = Thread.CurrentThread;
				IPrincipal principal2;
				lock (currentThread)
				{
					IPrincipal principal = CallContext.Principal;
					if (principal == null)
					{
						principal = Thread.GetDomain().GetThreadPrincipal();
						CallContext.Principal = principal;
					}
					principal2 = principal;
				}
				return principal2;
			}
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
			set
			{
				CallContext.Principal = value;
			}
		}

		// Token: 0x06003D87 RID: 15751 RVA: 0x000E6870 File Offset: 0x000E4A70
		[SecurityCritical]
		private void SetPrincipalInternal(IPrincipal principal)
		{
			this.GetMutableExecutionContext().LogicalCallContext.SecurityData.Principal = principal;
		}

		// Token: 0x06003D88 RID: 15752
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Context GetContextInternal(IntPtr id);

		// Token: 0x06003D89 RID: 15753
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern object InternalCrossContextCallback(Context ctx, IntPtr ctxID, int appDomainID, InternalCrossContextDelegate ftnToCall, object[] args);

		// Token: 0x06003D8A RID: 15754 RVA: 0x000E6888 File Offset: 0x000E4A88
		[SecurityCritical]
		internal object InternalCrossContextCallback(Context ctx, InternalCrossContextDelegate ftnToCall, object[] args)
		{
			return this.InternalCrossContextCallback(ctx, ctx.InternalContextID, 0, ftnToCall, args);
		}

		// Token: 0x06003D8B RID: 15755 RVA: 0x000E689A File Offset: 0x000E4A9A
		private static object CompleteCrossContextCallback(InternalCrossContextDelegate ftnToCall, object[] args)
		{
			return ftnToCall(args);
		}

		// Token: 0x06003D8C RID: 15756
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AppDomain GetDomainInternal();

		// Token: 0x06003D8D RID: 15757
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AppDomain GetFastDomainInternal();

		// Token: 0x06003D8E RID: 15758 RVA: 0x000E68A4 File Offset: 0x000E4AA4
		[SecuritySafeCritical]
		public static AppDomain GetDomain()
		{
			AppDomain appDomain = Thread.GetFastDomainInternal();
			if (appDomain == null)
			{
				appDomain = Thread.GetDomainInternal();
			}
			return appDomain;
		}

		// Token: 0x06003D8F RID: 15759 RVA: 0x000E68C1 File Offset: 0x000E4AC1
		public static int GetDomainID()
		{
			return Thread.GetDomain().GetId();
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06003D90 RID: 15760 RVA: 0x000E68CD File Offset: 0x000E4ACD
		// (set) Token: 0x06003D91 RID: 15761 RVA: 0x000E68D8 File Offset: 0x000E4AD8
		public string Name
		{
			get
			{
				return this.m_Name;
			}
			[SecuritySafeCritical]
			[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
			set
			{
				lock (this)
				{
					if (this.m_Name != null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WriteOnce"));
					}
					this.m_Name = value;
					Thread.InformThreadNameChange(this.GetNativeHandle(), value, (value != null) ? value.Length : 0);
				}
			}
		}

		// Token: 0x06003D92 RID: 15762
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void InformThreadNameChange(ThreadHandle t, string name, int len);

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06003D93 RID: 15763 RVA: 0x000E6944 File Offset: 0x000E4B44
		// (set) Token: 0x06003D94 RID: 15764 RVA: 0x000E6980 File Offset: 0x000E4B80
		internal object AbortReason
		{
			[SecurityCritical]
			get
			{
				object obj = null;
				try
				{
					obj = this.GetAbortReason();
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ExceptionStateCrossAppDomain"), ex);
				}
				return obj;
			}
			[SecurityCritical]
			set
			{
				this.SetAbortReason(value);
			}
		}

		// Token: 0x06003D95 RID: 15765
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BeginCriticalRegion();

		// Token: 0x06003D96 RID: 15766
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EndCriticalRegion();

		// Token: 0x06003D97 RID: 15767
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BeginThreadAffinity();

		// Token: 0x06003D98 RID: 15768
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EndThreadAffinity();

		// Token: 0x06003D99 RID: 15769 RVA: 0x000E698C File Offset: 0x000E4B8C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static byte VolatileRead(ref byte address)
		{
			byte b = address;
			Thread.MemoryBarrier();
			return b;
		}

		// Token: 0x06003D9A RID: 15770 RVA: 0x000E69A4 File Offset: 0x000E4BA4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static short VolatileRead(ref short address)
		{
			short num = address;
			Thread.MemoryBarrier();
			return num;
		}

		// Token: 0x06003D9B RID: 15771 RVA: 0x000E69BC File Offset: 0x000E4BBC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static int VolatileRead(ref int address)
		{
			int num = address;
			Thread.MemoryBarrier();
			return num;
		}

		// Token: 0x06003D9C RID: 15772 RVA: 0x000E69D4 File Offset: 0x000E4BD4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static long VolatileRead(ref long address)
		{
			long num = address;
			Thread.MemoryBarrier();
			return num;
		}

		// Token: 0x06003D9D RID: 15773 RVA: 0x000E69EC File Offset: 0x000E4BEC
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static sbyte VolatileRead(ref sbyte address)
		{
			sbyte b = address;
			Thread.MemoryBarrier();
			return b;
		}

		// Token: 0x06003D9E RID: 15774 RVA: 0x000E6A04 File Offset: 0x000E4C04
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static ushort VolatileRead(ref ushort address)
		{
			ushort num = address;
			Thread.MemoryBarrier();
			return num;
		}

		// Token: 0x06003D9F RID: 15775 RVA: 0x000E6A1C File Offset: 0x000E4C1C
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static uint VolatileRead(ref uint address)
		{
			uint num = address;
			Thread.MemoryBarrier();
			return num;
		}

		// Token: 0x06003DA0 RID: 15776 RVA: 0x000E6A34 File Offset: 0x000E4C34
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static IntPtr VolatileRead(ref IntPtr address)
		{
			IntPtr intPtr = address;
			Thread.MemoryBarrier();
			return intPtr;
		}

		// Token: 0x06003DA1 RID: 15777 RVA: 0x000E6A4C File Offset: 0x000E4C4C
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static UIntPtr VolatileRead(ref UIntPtr address)
		{
			UIntPtr uintPtr = address;
			Thread.MemoryBarrier();
			return uintPtr;
		}

		// Token: 0x06003DA2 RID: 15778 RVA: 0x000E6A64 File Offset: 0x000E4C64
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static ulong VolatileRead(ref ulong address)
		{
			ulong num = address;
			Thread.MemoryBarrier();
			return num;
		}

		// Token: 0x06003DA3 RID: 15779 RVA: 0x000E6A7C File Offset: 0x000E4C7C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static float VolatileRead(ref float address)
		{
			float num = address;
			Thread.MemoryBarrier();
			return num;
		}

		// Token: 0x06003DA4 RID: 15780 RVA: 0x000E6A94 File Offset: 0x000E4C94
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static double VolatileRead(ref double address)
		{
			double num = address;
			Thread.MemoryBarrier();
			return num;
		}

		// Token: 0x06003DA5 RID: 15781 RVA: 0x000E6AAC File Offset: 0x000E4CAC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static object VolatileRead(ref object address)
		{
			object obj = address;
			Thread.MemoryBarrier();
			return obj;
		}

		// Token: 0x06003DA6 RID: 15782 RVA: 0x000E6AC2 File Offset: 0x000E4CC2
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void VolatileWrite(ref byte address, byte value)
		{
			Thread.MemoryBarrier();
			address = value;
		}

		// Token: 0x06003DA7 RID: 15783 RVA: 0x000E6ACC File Offset: 0x000E4CCC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void VolatileWrite(ref short address, short value)
		{
			Thread.MemoryBarrier();
			address = value;
		}

		// Token: 0x06003DA8 RID: 15784 RVA: 0x000E6AD6 File Offset: 0x000E4CD6
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void VolatileWrite(ref int address, int value)
		{
			Thread.MemoryBarrier();
			address = value;
		}

		// Token: 0x06003DA9 RID: 15785 RVA: 0x000E6AE0 File Offset: 0x000E4CE0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void VolatileWrite(ref long address, long value)
		{
			Thread.MemoryBarrier();
			address = value;
		}

		// Token: 0x06003DAA RID: 15786 RVA: 0x000E6AEA File Offset: 0x000E4CEA
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void VolatileWrite(ref sbyte address, sbyte value)
		{
			Thread.MemoryBarrier();
			address = value;
		}

		// Token: 0x06003DAB RID: 15787 RVA: 0x000E6AF4 File Offset: 0x000E4CF4
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void VolatileWrite(ref ushort address, ushort value)
		{
			Thread.MemoryBarrier();
			address = value;
		}

		// Token: 0x06003DAC RID: 15788 RVA: 0x000E6AFE File Offset: 0x000E4CFE
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void VolatileWrite(ref uint address, uint value)
		{
			Thread.MemoryBarrier();
			address = value;
		}

		// Token: 0x06003DAD RID: 15789 RVA: 0x000E6B08 File Offset: 0x000E4D08
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void VolatileWrite(ref IntPtr address, IntPtr value)
		{
			Thread.MemoryBarrier();
			address = value;
		}

		// Token: 0x06003DAE RID: 15790 RVA: 0x000E6B12 File Offset: 0x000E4D12
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void VolatileWrite(ref UIntPtr address, UIntPtr value)
		{
			Thread.MemoryBarrier();
			address = value;
		}

		// Token: 0x06003DAF RID: 15791 RVA: 0x000E6B1C File Offset: 0x000E4D1C
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void VolatileWrite(ref ulong address, ulong value)
		{
			Thread.MemoryBarrier();
			address = value;
		}

		// Token: 0x06003DB0 RID: 15792 RVA: 0x000E6B26 File Offset: 0x000E4D26
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void VolatileWrite(ref float address, float value)
		{
			Thread.MemoryBarrier();
			address = value;
		}

		// Token: 0x06003DB1 RID: 15793 RVA: 0x000E6B30 File Offset: 0x000E4D30
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void VolatileWrite(ref double address, double value)
		{
			Thread.MemoryBarrier();
			address = value;
		}

		// Token: 0x06003DB2 RID: 15794 RVA: 0x000E6B3A File Offset: 0x000E4D3A
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void VolatileWrite(ref object address, object value)
		{
			Thread.MemoryBarrier();
			address = value;
		}

		// Token: 0x06003DB3 RID: 15795
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void MemoryBarrier();

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06003DB4 RID: 15796 RVA: 0x000E6B44 File Offset: 0x000E4D44
		private static LocalDataStoreMgr LocalDataStoreManager
		{
			get
			{
				if (Thread.s_LocalDataStoreMgr == null)
				{
					Interlocked.CompareExchange<LocalDataStoreMgr>(ref Thread.s_LocalDataStoreMgr, new LocalDataStoreMgr(), null);
				}
				return Thread.s_LocalDataStoreMgr;
			}
		}

		// Token: 0x06003DB5 RID: 15797 RVA: 0x000E6B63 File Offset: 0x000E4D63
		void _Thread.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003DB6 RID: 15798 RVA: 0x000E6B6A File Offset: 0x000E4D6A
		void _Thread.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003DB7 RID: 15799 RVA: 0x000E6B71 File Offset: 0x000E4D71
		void _Thread.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003DB8 RID: 15800 RVA: 0x000E6B78 File Offset: 0x000E4D78
		void _Thread.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003DB9 RID: 15801
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetAbortReason(object o);

		// Token: 0x06003DBA RID: 15802
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern object GetAbortReason();

		// Token: 0x06003DBB RID: 15803
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void ClearAbortReason();

		// Token: 0x040019E9 RID: 6633
		private Context m_Context;

		// Token: 0x040019EA RID: 6634
		private ExecutionContext m_ExecutionContext;

		// Token: 0x040019EB RID: 6635
		private string m_Name;

		// Token: 0x040019EC RID: 6636
		private Delegate m_Delegate;

		// Token: 0x040019ED RID: 6637
		private CultureInfo m_CurrentCulture;

		// Token: 0x040019EE RID: 6638
		private CultureInfo m_CurrentUICulture;

		// Token: 0x040019EF RID: 6639
		private object m_ThreadStartArg;

		// Token: 0x040019F0 RID: 6640
		private IntPtr DONT_USE_InternalThread;

		// Token: 0x040019F1 RID: 6641
		private int m_Priority;

		// Token: 0x040019F2 RID: 6642
		private int m_ManagedThreadId;

		// Token: 0x040019F3 RID: 6643
		private bool m_ExecutionContextBelongsToOuterScope;

		// Token: 0x040019F4 RID: 6644
		private static LocalDataStoreMgr s_LocalDataStoreMgr;

		// Token: 0x040019F5 RID: 6645
		[ThreadStatic]
		private static LocalDataStoreHolder s_LocalDataStore;

		// Token: 0x040019F6 RID: 6646
		private static AsyncLocal<CultureInfo> s_asyncLocalCurrentCulture;

		// Token: 0x040019F7 RID: 6647
		private static AsyncLocal<CultureInfo> s_asyncLocalCurrentUICulture;

		// Token: 0x040019F8 RID: 6648
		[ThreadStatic]
		private static int t_currentProcessorIdCache;

		// Token: 0x040019F9 RID: 6649
		private const int ProcessorIdCacheShift = 16;

		// Token: 0x040019FA RID: 6650
		private const int ProcessorIdCacheCountDownMask = 65535;

		// Token: 0x040019FB RID: 6651
		private const int ProcessorIdRefreshRate = 5000;
	}
}
