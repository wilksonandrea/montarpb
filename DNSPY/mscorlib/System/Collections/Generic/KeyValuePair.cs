using System;
using System.Text;

namespace System.Collections.Generic
{
	// Token: 0x020004DB RID: 1243
	[__DynamicallyInvokable]
	[Serializable]
	public struct KeyValuePair<TKey, TValue>
	{
		// Token: 0x06003AEB RID: 15083 RVA: 0x000DF870 File Offset: 0x000DDA70
		[__DynamicallyInvokable]
		public KeyValuePair(TKey key, TValue value)
		{
			this.key = key;
			this.value = value;
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06003AEC RID: 15084 RVA: 0x000DF880 File Offset: 0x000DDA80
		[__DynamicallyInvokable]
		public TKey Key
		{
			[__DynamicallyInvokable]
			get
			{
				return this.key;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06003AED RID: 15085 RVA: 0x000DF888 File Offset: 0x000DDA88
		[__DynamicallyInvokable]
		public TValue Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this.value;
			}
		}

		// Token: 0x06003AEE RID: 15086 RVA: 0x000DF890 File Offset: 0x000DDA90
		[__DynamicallyInvokable]
		public override string ToString()
		{
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			stringBuilder.Append('[');
			if (this.Key != null)
			{
				StringBuilder stringBuilder2 = stringBuilder;
				TKey tkey = this.Key;
				stringBuilder2.Append(tkey.ToString());
			}
			stringBuilder.Append(", ");
			if (this.Value != null)
			{
				StringBuilder stringBuilder3 = stringBuilder;
				TValue tvalue = this.Value;
				stringBuilder3.Append(tvalue.ToString());
			}
			stringBuilder.Append(']');
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x04001959 RID: 6489
		private TKey key;

		// Token: 0x0400195A RID: 6490
		private TValue value;
	}
}
