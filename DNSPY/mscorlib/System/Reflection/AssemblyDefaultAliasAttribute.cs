using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005BD RID: 1469
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyDefaultAliasAttribute : Attribute
	{
		// Token: 0x06004466 RID: 17510 RVA: 0x000FC239 File Offset: 0x000FA439
		[__DynamicallyInvokable]
		public AssemblyDefaultAliasAttribute(string defaultAlias)
		{
			this.m_defaultAlias = defaultAlias;
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06004467 RID: 17511 RVA: 0x000FC248 File Offset: 0x000FA448
		[__DynamicallyInvokable]
		public string DefaultAlias
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_defaultAlias;
			}
		}

		// Token: 0x04001C0C RID: 7180
		private string m_defaultAlias;
	}
}
