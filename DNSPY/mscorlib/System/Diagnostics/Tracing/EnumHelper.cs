using System;
using System.Reflection;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000440 RID: 1088
	internal static class EnumHelper<UnderlyingType>
	{
		// Token: 0x060035FB RID: 13819 RVA: 0x000D215C File Offset: 0x000D035C
		public static UnderlyingType Cast<ValueType>(ValueType value)
		{
			return EnumHelper<UnderlyingType>.Caster<ValueType>.Instance(value);
		}

		// Token: 0x060035FC RID: 13820 RVA: 0x000D2169 File Offset: 0x000D0369
		internal static UnderlyingType Identity(UnderlyingType value)
		{
			return value;
		}

		// Token: 0x060035FD RID: 13821 RVA: 0x000D216C File Offset: 0x000D036C
		// Note: this type is marked as 'beforefieldinit'.
		static EnumHelper()
		{
		}

		// Token: 0x0400181D RID: 6173
		private static readonly MethodInfo IdentityInfo = Statics.GetDeclaredStaticMethod(typeof(EnumHelper<UnderlyingType>), "Identity");

		// Token: 0x02000B9C RID: 2972
		// (Invoke) Token: 0x06006C9C RID: 27804
		private delegate UnderlyingType Transformer<ValueType>(ValueType value);

		// Token: 0x02000B9D RID: 2973
		private static class Caster<ValueType>
		{
			// Token: 0x06006C9F RID: 27807 RVA: 0x00177D0A File Offset: 0x00175F0A
			// Note: this type is marked as 'beforefieldinit'.
			static Caster()
			{
			}

			// Token: 0x04003535 RID: 13621
			public static readonly EnumHelper<UnderlyingType>.Transformer<ValueType> Instance = (EnumHelper<UnderlyingType>.Transformer<ValueType>)Statics.CreateDelegate(typeof(EnumHelper<UnderlyingType>.Transformer<ValueType>), EnumHelper<UnderlyingType>.IdentityInfo);
		}
	}
}
