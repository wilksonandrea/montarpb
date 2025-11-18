using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000942 RID: 2370
	[Obsolete("This attribute has been deprecated.  Application Domains no longer respect Activation Context boundaries in IDispatch calls.", false)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class SetWin32ContextInIDispatchAttribute : Attribute
	{
		// Token: 0x06006067 RID: 24679 RVA: 0x0014BDE3 File Offset: 0x00149FE3
		public SetWin32ContextInIDispatchAttribute()
		{
		}
	}
}
