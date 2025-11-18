using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000921 RID: 2337
	[Obsolete("The IDispatchImplAttribute is deprecated.", false)]
	[ComVisible(true)]
	[Serializable]
	public enum IDispatchImplType
	{
		// Token: 0x04002A7F RID: 10879
		SystemDefinedImpl,
		// Token: 0x04002A80 RID: 10880
		InternalImpl,
		// Token: 0x04002A81 RID: 10881
		CompatibleImpl
	}
}
