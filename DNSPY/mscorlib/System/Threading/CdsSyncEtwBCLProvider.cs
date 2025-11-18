using System;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x0200053C RID: 1340
	[FriendAccessAllowed]
	[EventSource(Name = "System.Threading.SynchronizationEventSource", Guid = "EC631D38-466B-4290-9306-834971BA0217", LocalizationResources = "mscorlib")]
	internal sealed class CdsSyncEtwBCLProvider : EventSource
	{
		// Token: 0x06003EDF RID: 16095 RVA: 0x000E9BDD File Offset: 0x000E7DDD
		private CdsSyncEtwBCLProvider()
		{
		}

		// Token: 0x06003EE0 RID: 16096 RVA: 0x000E9BE5 File Offset: 0x000E7DE5
		[Event(1, Level = EventLevel.Warning)]
		public void SpinLock_FastPathFailed(int ownerID)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(1, ownerID);
			}
		}

		// Token: 0x06003EE1 RID: 16097 RVA: 0x000E9BFA File Offset: 0x000E7DFA
		[Event(2, Level = EventLevel.Informational)]
		public void SpinWait_NextSpinWillYield()
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				base.WriteEvent(2);
			}
		}

		// Token: 0x06003EE2 RID: 16098 RVA: 0x000E9C10 File Offset: 0x000E7E10
		[SecuritySafeCritical]
		[Event(3, Level = EventLevel.Verbose, Version = 1)]
		public unsafe void Barrier_PhaseFinished(bool currentSense, long phaseNum)
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				EventSource.EventData* ptr;
				int num;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData)];
					num = (currentSense ? 1 : 0);
					ptr->Size = 4;
				}
				ptr->DataPointer = (IntPtr)((void*)(&num));
				ptr[1].Size = 8;
				ptr[1].DataPointer = (IntPtr)((void*)(&phaseNum));
				base.WriteEventCore(3, 2, ptr);
			}
		}

		// Token: 0x06003EE3 RID: 16099 RVA: 0x000E9C7D File Offset: 0x000E7E7D
		// Note: this type is marked as 'beforefieldinit'.
		static CdsSyncEtwBCLProvider()
		{
		}

		// Token: 0x04001A6E RID: 6766
		public static CdsSyncEtwBCLProvider Log = new CdsSyncEtwBCLProvider();

		// Token: 0x04001A6F RID: 6767
		private const EventKeywords ALL_KEYWORDS = EventKeywords.All;

		// Token: 0x04001A70 RID: 6768
		private const int SPINLOCK_FASTPATHFAILED_ID = 1;

		// Token: 0x04001A71 RID: 6769
		private const int SPINWAIT_NEXTSPINWILLYIELD_ID = 2;

		// Token: 0x04001A72 RID: 6770
		private const int BARRIER_PHASEFINISHED_ID = 3;
	}
}
