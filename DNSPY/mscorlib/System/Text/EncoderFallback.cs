using System;
using System.Threading;

namespace System.Text
{
	// Token: 0x02000A71 RID: 2673
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class EncoderFallback
	{
		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x06006806 RID: 26630 RVA: 0x0015F318 File Offset: 0x0015D518
		private static object InternalSyncObject
		{
			get
			{
				if (EncoderFallback.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref EncoderFallback.s_InternalSyncObject, obj, null);
				}
				return EncoderFallback.s_InternalSyncObject;
			}
		}

		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x06006807 RID: 26631 RVA: 0x0015F344 File Offset: 0x0015D544
		[__DynamicallyInvokable]
		public static EncoderFallback ReplacementFallback
		{
			[__DynamicallyInvokable]
			get
			{
				if (EncoderFallback.replacementFallback == null)
				{
					object internalSyncObject = EncoderFallback.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (EncoderFallback.replacementFallback == null)
						{
							EncoderFallback.replacementFallback = new EncoderReplacementFallback();
						}
					}
				}
				return EncoderFallback.replacementFallback;
			}
		}

		// Token: 0x170011BF RID: 4543
		// (get) Token: 0x06006808 RID: 26632 RVA: 0x0015F3A4 File Offset: 0x0015D5A4
		[__DynamicallyInvokable]
		public static EncoderFallback ExceptionFallback
		{
			[__DynamicallyInvokable]
			get
			{
				if (EncoderFallback.exceptionFallback == null)
				{
					object internalSyncObject = EncoderFallback.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (EncoderFallback.exceptionFallback == null)
						{
							EncoderFallback.exceptionFallback = new EncoderExceptionFallback();
						}
					}
				}
				return EncoderFallback.exceptionFallback;
			}
		}

		// Token: 0x06006809 RID: 26633
		[__DynamicallyInvokable]
		public abstract EncoderFallbackBuffer CreateFallbackBuffer();

		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x0600680A RID: 26634
		[__DynamicallyInvokable]
		public abstract int MaxCharCount
		{
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x0600680B RID: 26635 RVA: 0x0015F404 File Offset: 0x0015D604
		[__DynamicallyInvokable]
		protected EncoderFallback()
		{
		}

		// Token: 0x04002E6E RID: 11886
		internal bool bIsMicrosoftBestFitFallback;

		// Token: 0x04002E6F RID: 11887
		private static volatile EncoderFallback replacementFallback;

		// Token: 0x04002E70 RID: 11888
		private static volatile EncoderFallback exceptionFallback;

		// Token: 0x04002E71 RID: 11889
		private static object s_InternalSyncObject;
	}
}
