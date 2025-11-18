using System;
using System.Runtime.Serialization;

namespace System.Text
{
	// Token: 0x02000A70 RID: 2672
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class EncoderFallbackException : ArgumentException
	{
		// Token: 0x060067FB RID: 26619 RVA: 0x0015F1DE File Offset: 0x0015D3DE
		[__DynamicallyInvokable]
		public EncoderFallbackException()
			: base(Environment.GetResourceString("Arg_ArgumentException"))
		{
			base.SetErrorCode(-2147024809);
		}

		// Token: 0x060067FC RID: 26620 RVA: 0x0015F1FB File Offset: 0x0015D3FB
		[__DynamicallyInvokable]
		public EncoderFallbackException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024809);
		}

		// Token: 0x060067FD RID: 26621 RVA: 0x0015F20F File Offset: 0x0015D40F
		[__DynamicallyInvokable]
		public EncoderFallbackException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147024809);
		}

		// Token: 0x060067FE RID: 26622 RVA: 0x0015F224 File Offset: 0x0015D424
		internal EncoderFallbackException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060067FF RID: 26623 RVA: 0x0015F22E File Offset: 0x0015D42E
		internal EncoderFallbackException(string message, char charUnknown, int index)
			: base(message)
		{
			this.charUnknown = charUnknown;
			this.index = index;
		}

		// Token: 0x06006800 RID: 26624 RVA: 0x0015F248 File Offset: 0x0015D448
		internal EncoderFallbackException(string message, char charUnknownHigh, char charUnknownLow, int index)
			: base(message)
		{
			if (!char.IsHighSurrogate(charUnknownHigh))
			{
				throw new ArgumentOutOfRangeException("charUnknownHigh", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[] { 55296, 56319 }));
			}
			if (!char.IsLowSurrogate(charUnknownLow))
			{
				throw new ArgumentOutOfRangeException("CharUnknownLow", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[] { 56320, 57343 }));
			}
			this.charUnknownHigh = charUnknownHigh;
			this.charUnknownLow = charUnknownLow;
			this.index = index;
		}

		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x06006801 RID: 26625 RVA: 0x0015F2EC File Offset: 0x0015D4EC
		[__DynamicallyInvokable]
		public char CharUnknown
		{
			[__DynamicallyInvokable]
			get
			{
				return this.charUnknown;
			}
		}

		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x06006802 RID: 26626 RVA: 0x0015F2F4 File Offset: 0x0015D4F4
		[__DynamicallyInvokable]
		public char CharUnknownHigh
		{
			[__DynamicallyInvokable]
			get
			{
				return this.charUnknownHigh;
			}
		}

		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x06006803 RID: 26627 RVA: 0x0015F2FC File Offset: 0x0015D4FC
		[__DynamicallyInvokable]
		public char CharUnknownLow
		{
			[__DynamicallyInvokable]
			get
			{
				return this.charUnknownLow;
			}
		}

		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x06006804 RID: 26628 RVA: 0x0015F304 File Offset: 0x0015D504
		[__DynamicallyInvokable]
		public int Index
		{
			[__DynamicallyInvokable]
			get
			{
				return this.index;
			}
		}

		// Token: 0x06006805 RID: 26629 RVA: 0x0015F30C File Offset: 0x0015D50C
		[__DynamicallyInvokable]
		public bool IsUnknownSurrogate()
		{
			return this.charUnknownHigh > '\0';
		}

		// Token: 0x04002E6A RID: 11882
		private char charUnknown;

		// Token: 0x04002E6B RID: 11883
		private char charUnknownHigh;

		// Token: 0x04002E6C RID: 11884
		private char charUnknownLow;

		// Token: 0x04002E6D RID: 11885
		private int index;
	}
}
