using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008E1 RID: 2273
	[AttributeUsage(AttributeTargets.All)]
	[ComVisible(false)]
	internal sealed class DecoratedNameAttribute : Attribute
	{
		// Token: 0x06005DDD RID: 24029 RVA: 0x001497BB File Offset: 0x001479BB
		public DecoratedNameAttribute(string decoratedName)
		{
		}
	}
}
