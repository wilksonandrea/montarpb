using System;
using System.Threading;

namespace System.Text
{
	// Token: 0x02000A66 RID: 2662
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class DecoderFallback
	{
		// Token: 0x170011A5 RID: 4517
		// (get) Token: 0x060067A6 RID: 26534 RVA: 0x0015DF2C File Offset: 0x0015C12C
		private static object InternalSyncObject
		{
			get
			{
				if (DecoderFallback.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref DecoderFallback.s_InternalSyncObject, obj, null);
				}
				return DecoderFallback.s_InternalSyncObject;
			}
		}

		// Token: 0x170011A6 RID: 4518
		// (get) Token: 0x060067A7 RID: 26535 RVA: 0x0015DF58 File Offset: 0x0015C158
		[__DynamicallyInvokable]
		public static DecoderFallback ReplacementFallback
		{
			[__DynamicallyInvokable]
			get
			{
				if (DecoderFallback.replacementFallback == null)
				{
					object internalSyncObject = DecoderFallback.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (DecoderFallback.replacementFallback == null)
						{
							DecoderFallback.replacementFallback = new DecoderReplacementFallback();
						}
					}
				}
				return DecoderFallback.replacementFallback;
			}
		}

		// Token: 0x170011A7 RID: 4519
		// (get) Token: 0x060067A8 RID: 26536 RVA: 0x0015DFB8 File Offset: 0x0015C1B8
		[__DynamicallyInvokable]
		public static DecoderFallback ExceptionFallback
		{
			[__DynamicallyInvokable]
			get
			{
				if (DecoderFallback.exceptionFallback == null)
				{
					object internalSyncObject = DecoderFallback.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (DecoderFallback.exceptionFallback == null)
						{
							DecoderFallback.exceptionFallback = new DecoderExceptionFallback();
						}
					}
				}
				return DecoderFallback.exceptionFallback;
			}
		}

		// Token: 0x060067A9 RID: 26537
		[__DynamicallyInvokable]
		public abstract DecoderFallbackBuffer CreateFallbackBuffer();

		// Token: 0x170011A8 RID: 4520
		// (get) Token: 0x060067AA RID: 26538
		[__DynamicallyInvokable]
		public abstract int MaxCharCount
		{
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x170011A9 RID: 4521
		// (get) Token: 0x060067AB RID: 26539 RVA: 0x0015E018 File Offset: 0x0015C218
		internal bool IsMicrosoftBestFitFallback
		{
			get
			{
				return this.bIsMicrosoftBestFitFallback;
			}
		}

		// Token: 0x060067AC RID: 26540 RVA: 0x0015E020 File Offset: 0x0015C220
		[__DynamicallyInvokable]
		protected DecoderFallback()
		{
		}

		// Token: 0x04002E52 RID: 11858
		internal bool bIsMicrosoftBestFitFallback;

		// Token: 0x04002E53 RID: 11859
		private static volatile DecoderFallback replacementFallback;

		// Token: 0x04002E54 RID: 11860
		private static volatile DecoderFallback exceptionFallback;

		// Token: 0x04002E55 RID: 11861
		private static object s_InternalSyncObject;
	}
}
