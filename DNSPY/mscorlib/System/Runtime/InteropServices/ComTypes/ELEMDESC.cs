using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A46 RID: 2630
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct ELEMDESC
	{
		// Token: 0x04002DBD RID: 11709
		[__DynamicallyInvokable]
		public TYPEDESC tdesc;

		// Token: 0x04002DBE RID: 11710
		[__DynamicallyInvokable]
		public ELEMDESC.DESCUNION desc;

		// Token: 0x02000CAC RID: 3244
		[__DynamicallyInvokable]
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct DESCUNION
		{
			// Token: 0x04003894 RID: 14484
			[__DynamicallyInvokable]
			[FieldOffset(0)]
			public IDLDESC idldesc;

			// Token: 0x04003895 RID: 14485
			[__DynamicallyInvokable]
			[FieldOffset(0)]
			public PARAMDESC paramdesc;
		}
	}
}
