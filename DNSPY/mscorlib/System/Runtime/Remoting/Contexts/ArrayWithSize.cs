using System;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000813 RID: 2067
	internal class ArrayWithSize
	{
		// Token: 0x060058E0 RID: 22752 RVA: 0x001390EA File Offset: 0x001372EA
		internal ArrayWithSize(IDynamicMessageSink[] sinks, int count)
		{
			this.Sinks = sinks;
			this.Count = count;
		}

		// Token: 0x04002881 RID: 10369
		internal IDynamicMessageSink[] Sinks;

		// Token: 0x04002882 RID: 10370
		internal int Count;
	}
}
