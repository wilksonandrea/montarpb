using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	// Token: 0x020001C3 RID: 451
	[AttributeUsage(AttributeTargets.Module, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	public sealed class UnverifiableCodeAttribute : Attribute
	{
		// Token: 0x06001C1E RID: 7198 RVA: 0x00060BD9 File Offset: 0x0005EDD9
		public UnverifiableCodeAttribute()
		{
		}
	}
}
