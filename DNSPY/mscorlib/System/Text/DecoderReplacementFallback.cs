using System;

namespace System.Text
{
	// Token: 0x02000A68 RID: 2664
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class DecoderReplacementFallback : DecoderFallback
	{
		// Token: 0x060067B8 RID: 26552 RVA: 0x0015E21E File Offset: 0x0015C41E
		[__DynamicallyInvokable]
		public DecoderReplacementFallback()
			: this("?")
		{
		}

		// Token: 0x060067B9 RID: 26553 RVA: 0x0015E22C File Offset: 0x0015C42C
		[__DynamicallyInvokable]
		public DecoderReplacementFallback(string replacement)
		{
			if (replacement == null)
			{
				throw new ArgumentNullException("replacement");
			}
			bool flag = false;
			for (int i = 0; i < replacement.Length; i++)
			{
				if (char.IsSurrogate(replacement, i))
				{
					if (char.IsHighSurrogate(replacement, i))
					{
						if (flag)
						{
							break;
						}
						flag = true;
					}
					else
					{
						if (!flag)
						{
							flag = true;
							break;
						}
						flag = false;
					}
				}
				else if (flag)
				{
					break;
				}
			}
			if (flag)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex", new object[] { "replacement" }));
			}
			this.strDefault = replacement;
		}

		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x060067BA RID: 26554 RVA: 0x0015E2AF File Offset: 0x0015C4AF
		[__DynamicallyInvokable]
		public string DefaultString
		{
			[__DynamicallyInvokable]
			get
			{
				return this.strDefault;
			}
		}

		// Token: 0x060067BB RID: 26555 RVA: 0x0015E2B7 File Offset: 0x0015C4B7
		[__DynamicallyInvokable]
		public override DecoderFallbackBuffer CreateFallbackBuffer()
		{
			return new DecoderReplacementFallbackBuffer(this);
		}

		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x060067BC RID: 26556 RVA: 0x0015E2BF File Offset: 0x0015C4BF
		[__DynamicallyInvokable]
		public override int MaxCharCount
		{
			[__DynamicallyInvokable]
			get
			{
				return this.strDefault.Length;
			}
		}

		// Token: 0x060067BD RID: 26557 RVA: 0x0015E2CC File Offset: 0x0015C4CC
		[__DynamicallyInvokable]
		public override bool Equals(object value)
		{
			DecoderReplacementFallback decoderReplacementFallback = value as DecoderReplacementFallback;
			return decoderReplacementFallback != null && this.strDefault == decoderReplacementFallback.strDefault;
		}

		// Token: 0x060067BE RID: 26558 RVA: 0x0015E2F6 File Offset: 0x0015C4F6
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.strDefault.GetHashCode();
		}

		// Token: 0x04002E58 RID: 11864
		private string strDefault;
	}
}
