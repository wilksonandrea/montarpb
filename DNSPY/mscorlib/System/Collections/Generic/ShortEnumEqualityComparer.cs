using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x020004C7 RID: 1223
	[Serializable]
	internal sealed class ShortEnumEqualityComparer<T> : EnumEqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x06003AA3 RID: 15011 RVA: 0x000DF442 File Offset: 0x000DD642
		public ShortEnumEqualityComparer()
		{
		}

		// Token: 0x06003AA4 RID: 15012 RVA: 0x000DF44A File Offset: 0x000DD64A
		public ShortEnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x06003AA5 RID: 15013 RVA: 0x000DF454 File Offset: 0x000DD654
		public override int GetHashCode(T obj)
		{
			int num = JitHelpers.UnsafeEnumCast<T>(obj);
			return ((short)num).GetHashCode();
		}
	}
}
