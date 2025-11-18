using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000DF RID: 223
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class EventArgs
	{
		// Token: 0x06000E75 RID: 3701 RVA: 0x0002CD1B File Offset: 0x0002AF1B
		[__DynamicallyInvokable]
		public EventArgs()
		{
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0002CD23 File Offset: 0x0002AF23
		// Note: this type is marked as 'beforefieldinit'.
		static EventArgs()
		{
		}

		// Token: 0x0400057C RID: 1404
		[__DynamicallyInvokable]
		public static readonly EventArgs Empty = new EventArgs();
	}
}
