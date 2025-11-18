using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000938 RID: 2360
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class FieldOffsetAttribute : Attribute
	{
		// Token: 0x0600604B RID: 24651 RVA: 0x0014BC4C File Offset: 0x00149E4C
		[SecurityCritical]
		internal static Attribute GetCustomAttribute(RuntimeFieldInfo field)
		{
			int num;
			if (field.DeclaringType != null && field.GetRuntimeModule().MetadataImport.GetFieldOffset(field.DeclaringType.MetadataToken, field.MetadataToken, out num))
			{
				return new FieldOffsetAttribute(num);
			}
			return null;
		}

		// Token: 0x0600604C RID: 24652 RVA: 0x0014BC97 File Offset: 0x00149E97
		[SecurityCritical]
		internal static bool IsDefined(RuntimeFieldInfo field)
		{
			return FieldOffsetAttribute.GetCustomAttribute(field) != null;
		}

		// Token: 0x0600604D RID: 24653 RVA: 0x0014BCA2 File Offset: 0x00149EA2
		[__DynamicallyInvokable]
		public FieldOffsetAttribute(int offset)
		{
			this._val = offset;
		}

		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x0600604E RID: 24654 RVA: 0x0014BCB1 File Offset: 0x00149EB1
		[__DynamicallyInvokable]
		public int Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002B28 RID: 11048
		internal int _val;
	}
}
