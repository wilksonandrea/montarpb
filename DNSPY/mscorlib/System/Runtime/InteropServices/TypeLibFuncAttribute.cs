using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000929 RID: 2345
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibFuncAttribute : Attribute
	{
		// Token: 0x0600601E RID: 24606 RVA: 0x0014B6C7 File Offset: 0x001498C7
		public TypeLibFuncAttribute(TypeLibFuncFlags flags)
		{
			this._val = flags;
		}

		// Token: 0x0600601F RID: 24607 RVA: 0x0014B6D6 File Offset: 0x001498D6
		public TypeLibFuncAttribute(short flags)
		{
			this._val = (TypeLibFuncFlags)flags;
		}

		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x06006020 RID: 24608 RVA: 0x0014B6E5 File Offset: 0x001498E5
		public TypeLibFuncFlags Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002AB0 RID: 10928
		internal TypeLibFuncFlags _val;
	}
}
