using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	// Token: 0x020001C2 RID: 450
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	public sealed class SuppressUnmanagedCodeSecurityAttribute : Attribute
	{
		// Token: 0x06001C1D RID: 7197 RVA: 0x00060BD1 File Offset: 0x0005EDD1
		public SuppressUnmanagedCodeSecurityAttribute()
		{
		}
	}
}
