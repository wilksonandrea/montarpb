using System;

namespace System.Text
{
	// Token: 0x02000A73 RID: 2675
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class EncoderReplacementFallback : EncoderFallback
	{
		// Token: 0x06006818 RID: 26648 RVA: 0x0015F5BF File Offset: 0x0015D7BF
		[__DynamicallyInvokable]
		public EncoderReplacementFallback()
			: this("?")
		{
		}

		// Token: 0x06006819 RID: 26649 RVA: 0x0015F5CC File Offset: 0x0015D7CC
		[__DynamicallyInvokable]
		public EncoderReplacementFallback(string replacement)
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

		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x0600681A RID: 26650 RVA: 0x0015F64F File Offset: 0x0015D84F
		[__DynamicallyInvokable]
		public string DefaultString
		{
			[__DynamicallyInvokable]
			get
			{
				return this.strDefault;
			}
		}

		// Token: 0x0600681B RID: 26651 RVA: 0x0015F657 File Offset: 0x0015D857
		[__DynamicallyInvokable]
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			return new EncoderReplacementFallbackBuffer(this);
		}

		// Token: 0x170011C3 RID: 4547
		// (get) Token: 0x0600681C RID: 26652 RVA: 0x0015F65F File Offset: 0x0015D85F
		[__DynamicallyInvokable]
		public override int MaxCharCount
		{
			[__DynamicallyInvokable]
			get
			{
				return this.strDefault.Length;
			}
		}

		// Token: 0x0600681D RID: 26653 RVA: 0x0015F66C File Offset: 0x0015D86C
		[__DynamicallyInvokable]
		public override bool Equals(object value)
		{
			EncoderReplacementFallback encoderReplacementFallback = value as EncoderReplacementFallback;
			return encoderReplacementFallback != null && this.strDefault == encoderReplacementFallback.strDefault;
		}

		// Token: 0x0600681E RID: 26654 RVA: 0x0015F696 File Offset: 0x0015D896
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.strDefault.GetHashCode();
		}

		// Token: 0x04002E7A RID: 11898
		private string strDefault;
	}
}
