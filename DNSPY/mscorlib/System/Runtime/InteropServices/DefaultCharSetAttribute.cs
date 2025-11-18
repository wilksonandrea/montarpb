using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000941 RID: 2369
	[AttributeUsage(AttributeTargets.Module, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class DefaultCharSetAttribute : Attribute
	{
		// Token: 0x06006065 RID: 24677 RVA: 0x0014BDCC File Offset: 0x00149FCC
		[__DynamicallyInvokable]
		public DefaultCharSetAttribute(CharSet charSet)
		{
			this._CharSet = charSet;
		}

		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x06006066 RID: 24678 RVA: 0x0014BDDB File Offset: 0x00149FDB
		[__DynamicallyInvokable]
		public CharSet CharSet
		{
			[__DynamicallyInvokable]
			get
			{
				return this._CharSet;
			}
		}

		// Token: 0x04002B38 RID: 11064
		internal CharSet _CharSet;
	}
}
