using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime
{
	// Token: 0x02000719 RID: 1817
	[__DynamicallyInvokable]
	public static class GCSettings
	{
		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x06005131 RID: 20785 RVA: 0x0011E3E4 File Offset: 0x0011C5E4
		// (set) Token: 0x06005132 RID: 20786 RVA: 0x0011E3EB File Offset: 0x0011C5EB
		[__DynamicallyInvokable]
		public static GCLatencyMode LatencyMode
		{
			[SecuritySafeCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			get
			{
				return (GCLatencyMode)GC.GetGCLatencyMode();
			}
			[SecurityCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
			set
			{
				if (value < GCLatencyMode.Batch || value > GCLatencyMode.SustainedLowLatency)
				{
					throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_Enum"));
				}
				if (GC.SetGCLatencyMode((int)value) == 1)
				{
					throw new InvalidOperationException("The NoGCRegion mode is in progress. End it and then set a different mode.");
				}
			}
		}

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x06005133 RID: 20787 RVA: 0x0011E419 File Offset: 0x0011C619
		// (set) Token: 0x06005134 RID: 20788 RVA: 0x0011E420 File Offset: 0x0011C620
		[__DynamicallyInvokable]
		public static GCLargeObjectHeapCompactionMode LargeObjectHeapCompactionMode
		{
			[SecuritySafeCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			get
			{
				return (GCLargeObjectHeapCompactionMode)GC.GetLOHCompactionMode();
			}
			[SecurityCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
			set
			{
				if (value < GCLargeObjectHeapCompactionMode.Default || value > GCLargeObjectHeapCompactionMode.CompactOnce)
				{
					throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_Enum"));
				}
				GC.SetLOHCompactionMode((int)value);
			}
		}

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06005135 RID: 20789 RVA: 0x0011E440 File Offset: 0x0011C640
		[__DynamicallyInvokable]
		public static bool IsServerGC
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				return GC.IsServerGC();
			}
		}

		// Token: 0x02000C65 RID: 3173
		private enum SetLatencyModeStatus
		{
			// Token: 0x040037C7 RID: 14279
			Succeeded,
			// Token: 0x040037C8 RID: 14280
			NoGCInProgress
		}
	}
}
