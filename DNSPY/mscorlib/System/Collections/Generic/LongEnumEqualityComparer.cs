using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x020004C8 RID: 1224
	[Serializable]
	internal sealed class LongEnumEqualityComparer<T> : EqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x06003AA6 RID: 15014 RVA: 0x000DF474 File Offset: 0x000DD674
		public override bool Equals(T x, T y)
		{
			long num = JitHelpers.UnsafeEnumCastLong<T>(x);
			long num2 = JitHelpers.UnsafeEnumCastLong<T>(y);
			return num == num2;
		}

		// Token: 0x06003AA7 RID: 15015 RVA: 0x000DF494 File Offset: 0x000DD694
		public override int GetHashCode(T obj)
		{
			return JitHelpers.UnsafeEnumCastLong<T>(obj).GetHashCode();
		}

		// Token: 0x06003AA8 RID: 15016 RVA: 0x000DF4B0 File Offset: 0x000DD6B0
		public override bool Equals(object obj)
		{
			LongEnumEqualityComparer<T> longEnumEqualityComparer = obj as LongEnumEqualityComparer<T>;
			return longEnumEqualityComparer != null;
		}

		// Token: 0x06003AA9 RID: 15017 RVA: 0x000DF4C8 File Offset: 0x000DD6C8
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}

		// Token: 0x06003AAA RID: 15018 RVA: 0x000DF4DA File Offset: 0x000DD6DA
		public LongEnumEqualityComparer()
		{
		}

		// Token: 0x06003AAB RID: 15019 RVA: 0x000DF4E2 File Offset: 0x000DD6E2
		public LongEnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x06003AAC RID: 15020 RVA: 0x000DF4EA File Offset: 0x000DD6EA
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.SetType(typeof(ObjectEqualityComparer<T>));
		}
	}
}
