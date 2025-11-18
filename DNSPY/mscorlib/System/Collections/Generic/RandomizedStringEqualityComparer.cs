using System;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x020004C9 RID: 1225
	internal sealed class RandomizedStringEqualityComparer : IEqualityComparer<string>, IEqualityComparer, IWellKnownStringEqualityComparer
	{
		// Token: 0x06003AAD RID: 15021 RVA: 0x000DF4FC File Offset: 0x000DD6FC
		public RandomizedStringEqualityComparer()
		{
			this._entropy = HashHelpers.GetEntropy();
		}

		// Token: 0x06003AAE RID: 15022 RVA: 0x000DF50F File Offset: 0x000DD70F
		public bool Equals(object x, object y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (x is string && y is string)
			{
				return this.Equals((string)x, (string)y);
			}
			ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArgumentForComparison);
			return false;
		}

		// Token: 0x06003AAF RID: 15023 RVA: 0x000DF549 File Offset: 0x000DD749
		public bool Equals(string x, string y)
		{
			if (x != null)
			{
				return y != null && x.Equals(y);
			}
			return y == null;
		}

		// Token: 0x06003AB0 RID: 15024 RVA: 0x000DF561 File Offset: 0x000DD761
		[SecuritySafeCritical]
		public int GetHashCode(string obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return string.InternalMarvin32HashString(obj, obj.Length, this._entropy);
		}

		// Token: 0x06003AB1 RID: 15025 RVA: 0x000DF57C File Offset: 0x000DD77C
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

		// Token: 0x06003AB2 RID: 15026 RVA: 0x000DF5B4 File Offset: 0x000DD7B4
		public override bool Equals(object obj)
		{
			RandomizedStringEqualityComparer randomizedStringEqualityComparer = obj as RandomizedStringEqualityComparer;
			return randomizedStringEqualityComparer != null && this._entropy == randomizedStringEqualityComparer._entropy;
		}

		// Token: 0x06003AB3 RID: 15027 RVA: 0x000DF5DB File Offset: 0x000DD7DB
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode() ^ (int)(this._entropy & 2147483647L);
		}

		// Token: 0x06003AB4 RID: 15028 RVA: 0x000DF5FC File Offset: 0x000DD7FC
		IEqualityComparer IWellKnownStringEqualityComparer.GetRandomizedEqualityComparer()
		{
			return new RandomizedStringEqualityComparer();
		}

		// Token: 0x06003AB5 RID: 15029 RVA: 0x000DF603 File Offset: 0x000DD803
		IEqualityComparer IWellKnownStringEqualityComparer.GetEqualityComparerForSerialization()
		{
			return EqualityComparer<string>.Default;
		}

		// Token: 0x04001952 RID: 6482
		private long _entropy;
	}
}
