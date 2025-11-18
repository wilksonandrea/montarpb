using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000939 RID: 2361
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComAliasNameAttribute : Attribute
	{
		// Token: 0x0600604F RID: 24655 RVA: 0x0014BCB9 File Offset: 0x00149EB9
		public ComAliasNameAttribute(string alias)
		{
			this._val = alias;
		}

		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x06006050 RID: 24656 RVA: 0x0014BCC8 File Offset: 0x00149EC8
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002B29 RID: 11049
		internal string _val;
	}
}
