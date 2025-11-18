using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200093A RID: 2362
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	public sealed class AutomationProxyAttribute : Attribute
	{
		// Token: 0x06006051 RID: 24657 RVA: 0x0014BCD0 File Offset: 0x00149ED0
		public AutomationProxyAttribute(bool val)
		{
			this._val = val;
		}

		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x06006052 RID: 24658 RVA: 0x0014BCDF File Offset: 0x00149EDF
		public bool Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002B2A RID: 11050
		internal bool _val;
	}
}
