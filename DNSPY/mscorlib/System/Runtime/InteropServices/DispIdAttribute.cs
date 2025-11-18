using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000914 RID: 2324
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class DispIdAttribute : Attribute
	{
		// Token: 0x06005FFB RID: 24571 RVA: 0x0014B46F File Offset: 0x0014966F
		[__DynamicallyInvokable]
		public DispIdAttribute(int dispId)
		{
			this._val = dispId;
		}

		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x06005FFC RID: 24572 RVA: 0x0014B47E File Offset: 0x0014967E
		[__DynamicallyInvokable]
		public int Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A6C RID: 10860
		internal int _val;
	}
}
