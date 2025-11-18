using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000928 RID: 2344
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibTypeAttribute : Attribute
	{
		// Token: 0x0600601B RID: 24603 RVA: 0x0014B6A1 File Offset: 0x001498A1
		public TypeLibTypeAttribute(TypeLibTypeFlags flags)
		{
			this._val = flags;
		}

		// Token: 0x0600601C RID: 24604 RVA: 0x0014B6B0 File Offset: 0x001498B0
		public TypeLibTypeAttribute(short flags)
		{
			this._val = (TypeLibTypeFlags)flags;
		}

		// Token: 0x170010DC RID: 4316
		// (get) Token: 0x0600601D RID: 24605 RVA: 0x0014B6BF File Offset: 0x001498BF
		public TypeLibTypeFlags Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002AAF RID: 10927
		internal TypeLibTypeFlags _val;
	}
}
