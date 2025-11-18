using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200092A RID: 2346
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibVarAttribute : Attribute
	{
		// Token: 0x06006021 RID: 24609 RVA: 0x0014B6ED File Offset: 0x001498ED
		public TypeLibVarAttribute(TypeLibVarFlags flags)
		{
			this._val = flags;
		}

		// Token: 0x06006022 RID: 24610 RVA: 0x0014B6FC File Offset: 0x001498FC
		public TypeLibVarAttribute(short flags)
		{
			this._val = (TypeLibVarFlags)flags;
		}

		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x06006023 RID: 24611 RVA: 0x0014B70B File Offset: 0x0014990B
		public TypeLibVarFlags Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002AB1 RID: 10929
		internal TypeLibVarFlags _val;
	}
}
