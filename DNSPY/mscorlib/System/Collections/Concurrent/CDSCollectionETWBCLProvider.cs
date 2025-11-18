using System;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;

namespace System.Collections.Concurrent
{
	// Token: 0x020004AD RID: 1197
	[FriendAccessAllowed]
	[EventSource(Name = "System.Collections.Concurrent.ConcurrentCollectionsEventSource", Guid = "35167F8E-49B2-4b96-AB86-435B59336B5E", LocalizationResources = "mscorlib")]
	internal sealed class CDSCollectionETWBCLProvider : EventSource
	{
		// Token: 0x0600392C RID: 14636 RVA: 0x000DA986 File Offset: 0x000D8B86
		private CDSCollectionETWBCLProvider()
		{
		}

		// Token: 0x0600392D RID: 14637 RVA: 0x000DA98E File Offset: 0x000D8B8E
		[Event(1, Level = EventLevel.Warning)]
		public void ConcurrentStack_FastPushFailed(int spinCount)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(1, spinCount);
			}
		}

		// Token: 0x0600392E RID: 14638 RVA: 0x000DA9A3 File Offset: 0x000D8BA3
		[Event(2, Level = EventLevel.Warning)]
		public void ConcurrentStack_FastPopFailed(int spinCount)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(2, spinCount);
			}
		}

		// Token: 0x0600392F RID: 14639 RVA: 0x000DA9B8 File Offset: 0x000D8BB8
		[Event(3, Level = EventLevel.Warning)]
		public void ConcurrentDictionary_AcquiringAllLocks(int numOfBuckets)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(3, numOfBuckets);
			}
		}

		// Token: 0x06003930 RID: 14640 RVA: 0x000DA9CD File Offset: 0x000D8BCD
		[Event(4, Level = EventLevel.Verbose)]
		public void ConcurrentBag_TryTakeSteals()
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				base.WriteEvent(4);
			}
		}

		// Token: 0x06003931 RID: 14641 RVA: 0x000DA9E1 File Offset: 0x000D8BE1
		[Event(5, Level = EventLevel.Verbose)]
		public void ConcurrentBag_TryPeekSteals()
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				base.WriteEvent(5);
			}
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x000DA9F5 File Offset: 0x000D8BF5
		// Note: this type is marked as 'beforefieldinit'.
		static CDSCollectionETWBCLProvider()
		{
		}

		// Token: 0x04001915 RID: 6421
		public static CDSCollectionETWBCLProvider Log = new CDSCollectionETWBCLProvider();

		// Token: 0x04001916 RID: 6422
		private const EventKeywords ALL_KEYWORDS = EventKeywords.All;

		// Token: 0x04001917 RID: 6423
		private const int CONCURRENTSTACK_FASTPUSHFAILED_ID = 1;

		// Token: 0x04001918 RID: 6424
		private const int CONCURRENTSTACK_FASTPOPFAILED_ID = 2;

		// Token: 0x04001919 RID: 6425
		private const int CONCURRENTDICTIONARY_ACQUIRINGALLLOCKS_ID = 3;

		// Token: 0x0400191A RID: 6426
		private const int CONCURRENTBAG_TRYTAKESTEALS_ID = 4;

		// Token: 0x0400191B RID: 6427
		private const int CONCURRENTBAG_TRYPEEKSTEALS_ID = 5;
	}
}
