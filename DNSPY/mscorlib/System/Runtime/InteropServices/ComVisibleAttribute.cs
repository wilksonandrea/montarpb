using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200091A RID: 2330
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class ComVisibleAttribute : Attribute
	{
		// Token: 0x06006005 RID: 24581 RVA: 0x0014B4E9 File Offset: 0x001496E9
		[__DynamicallyInvokable]
		public ComVisibleAttribute(bool visibility)
		{
			this._val = visibility;
		}

		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x06006006 RID: 24582 RVA: 0x0014B4F8 File Offset: 0x001496F8
		[__DynamicallyInvokable]
		public bool Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A79 RID: 10873
		internal bool _val;
	}
}
