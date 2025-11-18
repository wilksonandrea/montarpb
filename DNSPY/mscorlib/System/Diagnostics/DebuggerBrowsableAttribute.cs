using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	// Token: 0x020003ED RID: 1005
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class DebuggerBrowsableAttribute : Attribute
	{
		// Token: 0x06003314 RID: 13076 RVA: 0x000C4A5A File Offset: 0x000C2C5A
		[__DynamicallyInvokable]
		public DebuggerBrowsableAttribute(DebuggerBrowsableState state)
		{
			if (state < DebuggerBrowsableState.Never || state > DebuggerBrowsableState.RootHidden)
			{
				throw new ArgumentOutOfRangeException("state");
			}
			this.state = state;
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06003315 RID: 13077 RVA: 0x000C4A7C File Offset: 0x000C2C7C
		[__DynamicallyInvokable]
		public DebuggerBrowsableState State
		{
			[__DynamicallyInvokable]
			get
			{
				return this.state;
			}
		}

		// Token: 0x040016A8 RID: 5800
		private DebuggerBrowsableState state;
	}
}
