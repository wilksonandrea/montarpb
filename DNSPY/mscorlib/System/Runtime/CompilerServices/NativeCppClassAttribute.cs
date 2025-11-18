using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008E0 RID: 2272
	[AttributeUsage(AttributeTargets.Struct, Inherited = true)]
	[ComVisible(true)]
	[Serializable]
	public sealed class NativeCppClassAttribute : Attribute
	{
		// Token: 0x06005DDC RID: 24028 RVA: 0x001497B3 File Offset: 0x001479B3
		public NativeCppClassAttribute()
		{
		}
	}
}
