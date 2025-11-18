using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x020004C5 RID: 1221
	[Serializable]
	internal class EnumEqualityComparer<T> : EqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x06003A99 RID: 15001 RVA: 0x000DF370 File Offset: 0x000DD570
		public override bool Equals(T x, T y)
		{
			int num = JitHelpers.UnsafeEnumCast<T>(x);
			int num2 = JitHelpers.UnsafeEnumCast<T>(y);
			return num == num2;
		}

		// Token: 0x06003A9A RID: 15002 RVA: 0x000DF390 File Offset: 0x000DD590
		public override int GetHashCode(T obj)
		{
			return JitHelpers.UnsafeEnumCast<T>(obj).GetHashCode();
		}

		// Token: 0x06003A9B RID: 15003 RVA: 0x000DF3AB File Offset: 0x000DD5AB
		public EnumEqualityComparer()
		{
		}

		// Token: 0x06003A9C RID: 15004 RVA: 0x000DF3B3 File Offset: 0x000DD5B3
		protected EnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x06003A9D RID: 15005 RVA: 0x000DF3BB File Offset: 0x000DD5BB
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (Type.GetTypeCode(Enum.GetUnderlyingType(typeof(T))) != TypeCode.Int32)
			{
				info.SetType(typeof(ObjectEqualityComparer<T>));
			}
		}

		// Token: 0x06003A9E RID: 15006 RVA: 0x000DF3E8 File Offset: 0x000DD5E8
		public override bool Equals(object obj)
		{
			EnumEqualityComparer<T> enumEqualityComparer = obj as EnumEqualityComparer<T>;
			return enumEqualityComparer != null;
		}

		// Token: 0x06003A9F RID: 15007 RVA: 0x000DF400 File Offset: 0x000DD600
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
