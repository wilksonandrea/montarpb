using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008BA RID: 2234
	[AttributeUsage(AttributeTargets.Property, Inherited = true)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class IndexerNameAttribute : Attribute
	{
		// Token: 0x06005DB3 RID: 23987 RVA: 0x001495FC File Offset: 0x001477FC
		[__DynamicallyInvokable]
		public IndexerNameAttribute(string indexerName)
		{
		}
	}
}
