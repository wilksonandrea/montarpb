using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200092F RID: 2351
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class GuidAttribute : Attribute
	{
		// Token: 0x06006030 RID: 24624 RVA: 0x0014B8BA File Offset: 0x00149ABA
		[__DynamicallyInvokable]
		public GuidAttribute(string guid)
		{
			this._val = guid;
		}

		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x06006031 RID: 24625 RVA: 0x0014B8C9 File Offset: 0x00149AC9
		[__DynamicallyInvokable]
		public string Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002B10 RID: 11024
		internal string _val;
	}
}
