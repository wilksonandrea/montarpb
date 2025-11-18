using System;
using System.Diagnostics.Tracing;
using System.Security;

namespace System.Threading
{
	// Token: 0x0200050F RID: 1295
	[EventSource(Name = "Microsoft-DotNETRuntime-PinnableBufferCache")]
	internal sealed class PinnableBufferCacheEventSource : EventSource
	{
		// Token: 0x06003CDD RID: 15581 RVA: 0x000E5A04 File Offset: 0x000E3C04
		[Event(1, Level = EventLevel.Verbose)]
		public void DebugMessage(string message)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(1, message);
			}
		}

		// Token: 0x06003CDE RID: 15582 RVA: 0x000E5A16 File Offset: 0x000E3C16
		[Event(2, Level = EventLevel.Verbose)]
		public void DebugMessage1(string message, long value)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(2, message, value);
			}
		}

		// Token: 0x06003CDF RID: 15583 RVA: 0x000E5A29 File Offset: 0x000E3C29
		[Event(3, Level = EventLevel.Verbose)]
		public void DebugMessage2(string message, long value1, long value2)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(3, new object[] { message, value1, value2 });
			}
		}

		// Token: 0x06003CE0 RID: 15584 RVA: 0x000E5A56 File Offset: 0x000E3C56
		[Event(18, Level = EventLevel.Verbose)]
		public void DebugMessage3(string message, long value1, long value2, long value3)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(18, new object[] { message, value1, value2, value3 });
			}
		}

		// Token: 0x06003CE1 RID: 15585 RVA: 0x000E5A8E File Offset: 0x000E3C8E
		[Event(4)]
		public void Create(string cacheName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(4, cacheName);
			}
		}

		// Token: 0x06003CE2 RID: 15586 RVA: 0x000E5AA0 File Offset: 0x000E3CA0
		[Event(5, Level = EventLevel.Verbose)]
		public void AllocateBuffer(string cacheName, ulong objectId, int objectHash, int objectGen, int freeCountAfter)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(5, new object[] { cacheName, objectId, objectHash, objectGen, freeCountAfter });
			}
		}

		// Token: 0x06003CE3 RID: 15587 RVA: 0x000E5AEC File Offset: 0x000E3CEC
		[Event(6)]
		public void AllocateBufferFromNotGen2(string cacheName, int notGen2CountAfter)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(6, cacheName, notGen2CountAfter);
			}
		}

		// Token: 0x06003CE4 RID: 15588 RVA: 0x000E5AFF File Offset: 0x000E3CFF
		[Event(7)]
		public void AllocateBufferCreatingNewBuffers(string cacheName, int totalBuffsBefore, int objectCount)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(7, cacheName, totalBuffsBefore, objectCount);
			}
		}

		// Token: 0x06003CE5 RID: 15589 RVA: 0x000E5B13 File Offset: 0x000E3D13
		[Event(8)]
		public void AllocateBufferAged(string cacheName, int agedCount)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(8, cacheName, agedCount);
			}
		}

		// Token: 0x06003CE6 RID: 15590 RVA: 0x000E5B26 File Offset: 0x000E3D26
		[Event(9)]
		public void AllocateBufferFreeListEmpty(string cacheName, int notGen2CountBefore)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(9, cacheName, notGen2CountBefore);
			}
		}

		// Token: 0x06003CE7 RID: 15591 RVA: 0x000E5B3A File Offset: 0x000E3D3A
		[Event(10, Level = EventLevel.Verbose)]
		public void FreeBuffer(string cacheName, ulong objectId, int objectHash, int freeCountBefore)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(10, new object[] { cacheName, objectId, objectHash, freeCountBefore });
			}
		}

		// Token: 0x06003CE8 RID: 15592 RVA: 0x000E5B72 File Offset: 0x000E3D72
		[Event(11)]
		public void FreeBufferStillTooYoung(string cacheName, int notGen2CountBefore)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(11, cacheName, notGen2CountBefore);
			}
		}

		// Token: 0x06003CE9 RID: 15593 RVA: 0x000E5B86 File Offset: 0x000E3D86
		[Event(13)]
		public void TrimCheck(string cacheName, int totalBuffs, bool neededMoreThanFreeList, int deltaMSec)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(13, new object[] { cacheName, totalBuffs, neededMoreThanFreeList, deltaMSec });
			}
		}

		// Token: 0x06003CEA RID: 15594 RVA: 0x000E5BBE File Offset: 0x000E3DBE
		[Event(14)]
		public void TrimFree(string cacheName, int totalBuffs, int freeListCount, int toBeFreed)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(14, new object[] { cacheName, totalBuffs, freeListCount, toBeFreed });
			}
		}

		// Token: 0x06003CEB RID: 15595 RVA: 0x000E5BF6 File Offset: 0x000E3DF6
		[Event(15)]
		public void TrimExperiment(string cacheName, int totalBuffs, int freeListCount, int numTrimTrial)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(15, new object[] { cacheName, totalBuffs, freeListCount, numTrimTrial });
			}
		}

		// Token: 0x06003CEC RID: 15596 RVA: 0x000E5C2E File Offset: 0x000E3E2E
		[Event(16)]
		public void TrimFreeSizeOK(string cacheName, int totalBuffs, int freeListCount)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(16, cacheName, totalBuffs, freeListCount);
			}
		}

		// Token: 0x06003CED RID: 15597 RVA: 0x000E5C43 File Offset: 0x000E3E43
		[Event(17)]
		public void TrimFlush(string cacheName, int totalBuffs, int freeListCount, int notGen2CountBefore)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(17, new object[] { cacheName, totalBuffs, freeListCount, notGen2CountBefore });
			}
		}

		// Token: 0x06003CEE RID: 15598 RVA: 0x000E5C7B File Offset: 0x000E3E7B
		[Event(20)]
		public void AgePendingBuffersResults(string cacheName, int promotedToFreeListCount, int heldBackCount)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(20, cacheName, promotedToFreeListCount, heldBackCount);
			}
		}

		// Token: 0x06003CEF RID: 15599 RVA: 0x000E5C90 File Offset: 0x000E3E90
		[Event(21)]
		public void WalkFreeListResult(string cacheName, int freeListCount, int gen0BuffersInFreeList)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(21, cacheName, freeListCount, gen0BuffersInFreeList);
			}
		}

		// Token: 0x06003CF0 RID: 15600 RVA: 0x000E5CA5 File Offset: 0x000E3EA5
		[Event(22)]
		public void FreeBufferNull(string cacheName, int freeCountBefore)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(22, cacheName, freeCountBefore);
			}
		}

		// Token: 0x06003CF1 RID: 15601 RVA: 0x000E5CBC File Offset: 0x000E3EBC
		internal static ulong AddressOf(object obj)
		{
			byte[] array = obj as byte[];
			if (array != null)
			{
				return (ulong)PinnableBufferCacheEventSource.AddressOfByteArray(array);
			}
			return 0UL;
		}

		// Token: 0x06003CF2 RID: 15602 RVA: 0x000E5CDC File Offset: 0x000E3EDC
		[SecuritySafeCritical]
		internal unsafe static long AddressOfByteArray(byte[] array)
		{
			if (array == null)
			{
				return 0L;
			}
			byte* ptr;
			if (array == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			return ptr - 2 * sizeof(void*);
		}

		// Token: 0x06003CF3 RID: 15603 RVA: 0x000E5D12 File Offset: 0x000E3F12
		public PinnableBufferCacheEventSource()
		{
		}

		// Token: 0x06003CF4 RID: 15604 RVA: 0x000E5D1A File Offset: 0x000E3F1A
		// Note: this type is marked as 'beforefieldinit'.
		static PinnableBufferCacheEventSource()
		{
		}

		// Token: 0x040019DA RID: 6618
		public static readonly PinnableBufferCacheEventSource Log = new PinnableBufferCacheEventSource();
	}
}
