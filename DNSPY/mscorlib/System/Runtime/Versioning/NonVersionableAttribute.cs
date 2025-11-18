using System;
using System.Diagnostics;

namespace System.Runtime.Versioning
{
	// Token: 0x0200072A RID: 1834
	[Conditional("FEATURE_READYTORUN")]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	internal sealed class NonVersionableAttribute : Attribute
	{
		// Token: 0x06005177 RID: 20855 RVA: 0x0011EF26 File Offset: 0x0011D126
		public NonVersionableAttribute()
		{
		}
	}
}
