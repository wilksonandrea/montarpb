using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008FF RID: 2303
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class IUnknownConstantAttribute : CustomConstantAttribute
	{
		// Token: 0x06005E59 RID: 24153 RVA: 0x0014B34F File Offset: 0x0014954F
		public IUnknownConstantAttribute()
		{
		}

		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x06005E5A RID: 24154 RVA: 0x0014B357 File Offset: 0x00149557
		public override object Value
		{
			get
			{
				return new UnknownWrapper(null);
			}
		}
	}
}
