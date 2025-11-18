using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008FE RID: 2302
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class IDispatchConstantAttribute : CustomConstantAttribute
	{
		// Token: 0x06005E57 RID: 24151 RVA: 0x0014B33F File Offset: 0x0014953F
		public IDispatchConstantAttribute()
		{
		}

		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x06005E58 RID: 24152 RVA: 0x0014B347 File Offset: 0x00149547
		public override object Value
		{
			get
			{
				return new DispatchWrapper(null);
			}
		}
	}
}
