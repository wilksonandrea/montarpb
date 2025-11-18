using System;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x020004CA RID: 1226
	internal sealed class RandomizedObjectEqualityComparer : IEqualityComparer, IWellKnownStringEqualityComparer
	{
		// Token: 0x06003AB6 RID: 15030 RVA: 0x000DF60A File Offset: 0x000DD80A
		public RandomizedObjectEqualityComparer()
		{
			this._entropy = HashHelpers.GetEntropy();
		}

		// Token: 0x06003AB7 RID: 15031 RVA: 0x000DF61D File Offset: 0x000DD81D
		public bool Equals(object x, object y)
		{
			if (x != null)
			{
				return y != null && x.Equals(y);
			}
			return y == null;
		}

		// Token: 0x06003AB8 RID: 15032 RVA: 0x000DF638 File Offset: 0x000DD838
		[SecuritySafeCritical]
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				return 0;
			}
			string text = obj as string;
			if (text != null)
			{
				return string.InternalMarvin32HashString(text, text.Length, this._entropy);
			}
			return obj.GetHashCode();
		}

		// Token: 0x06003AB9 RID: 15033 RVA: 0x000DF670 File Offset: 0x000DD870
		public override bool Equals(object obj)
		{
			RandomizedObjectEqualityComparer randomizedObjectEqualityComparer = obj as RandomizedObjectEqualityComparer;
			return randomizedObjectEqualityComparer != null && this._entropy == randomizedObjectEqualityComparer._entropy;
		}

		// Token: 0x06003ABA RID: 15034 RVA: 0x000DF697 File Offset: 0x000DD897
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode() ^ (int)(this._entropy & 2147483647L);
		}

		// Token: 0x06003ABB RID: 15035 RVA: 0x000DF6B8 File Offset: 0x000DD8B8
		IEqualityComparer IWellKnownStringEqualityComparer.GetRandomizedEqualityComparer()
		{
			return new RandomizedObjectEqualityComparer();
		}

		// Token: 0x06003ABC RID: 15036 RVA: 0x000DF6BF File Offset: 0x000DD8BF
		IEqualityComparer IWellKnownStringEqualityComparer.GetEqualityComparerForSerialization()
		{
			return null;
		}

		// Token: 0x04001953 RID: 6483
		private long _entropy;
	}
}
