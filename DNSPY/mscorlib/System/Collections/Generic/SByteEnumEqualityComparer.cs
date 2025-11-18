using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x020004C6 RID: 1222
	[Serializable]
	internal sealed class SByteEnumEqualityComparer<T> : EnumEqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x06003AA0 RID: 15008 RVA: 0x000DF412 File Offset: 0x000DD612
		public SByteEnumEqualityComparer()
		{
		}

		// Token: 0x06003AA1 RID: 15009 RVA: 0x000DF41A File Offset: 0x000DD61A
		public SByteEnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x06003AA2 RID: 15010 RVA: 0x000DF424 File Offset: 0x000DD624
		public override int GetHashCode(T obj)
		{
			int num = JitHelpers.UnsafeEnumCast<T>(obj);
			return ((sbyte)num).GetHashCode();
		}
	}
}
