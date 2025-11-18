using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
	// Token: 0x020000CF RID: 207
	[Serializable]
	internal struct Currency
	{
		// Token: 0x06000CE8 RID: 3304 RVA: 0x00027676 File Offset: 0x00025876
		public Currency(decimal value)
		{
			this.m_value = decimal.ToCurrency(value).m_value;
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x00027689 File Offset: 0x00025889
		internal Currency(long value, int ignored)
		{
			this.m_value = value;
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x00027692 File Offset: 0x00025892
		public static Currency FromOACurrency(long cy)
		{
			return new Currency(cy, 0);
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0002769B File Offset: 0x0002589B
		public long ToOACurrency()
		{
			return this.m_value;
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x000276A4 File Offset: 0x000258A4
		[SecuritySafeCritical]
		public static decimal ToDecimal(Currency c)
		{
			decimal num = 0m;
			Currency.FCallToDecimal(ref num, c);
			return num;
		}

		// Token: 0x06000CED RID: 3309
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FCallToDecimal(ref decimal result, Currency c);

		// Token: 0x04000539 RID: 1337
		internal long m_value;
	}
}
