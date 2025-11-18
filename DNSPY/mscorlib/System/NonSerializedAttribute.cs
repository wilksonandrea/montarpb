using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000113 RID: 275
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[ComVisible(true)]
	public sealed class NonSerializedAttribute : Attribute
	{
		// Token: 0x06001073 RID: 4211 RVA: 0x0003155C File Offset: 0x0002F75C
		internal static Attribute GetCustomAttribute(RuntimeFieldInfo field)
		{
			if ((field.Attributes & FieldAttributes.NotSerialized) == FieldAttributes.PrivateScope)
			{
				return null;
			}
			return new NonSerializedAttribute();
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x00031573 File Offset: 0x0002F773
		internal static bool IsDefined(RuntimeFieldInfo field)
		{
			return (field.Attributes & FieldAttributes.NotSerialized) > FieldAttributes.PrivateScope;
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x00031584 File Offset: 0x0002F784
		public NonSerializedAttribute()
		{
		}
	}
}
