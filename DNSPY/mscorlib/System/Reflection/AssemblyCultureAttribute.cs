using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005C0 RID: 1472
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyCultureAttribute : Attribute
	{
		// Token: 0x0600446C RID: 17516 RVA: 0x000FC28C File Offset: 0x000FA48C
		[__DynamicallyInvokable]
		public AssemblyCultureAttribute(string culture)
		{
			this.m_culture = culture;
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x0600446D RID: 17517 RVA: 0x000FC29B File Offset: 0x000FA49B
		[__DynamicallyInvokable]
		public string Culture
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_culture;
			}
		}

		// Token: 0x04001C0F RID: 7183
		private string m_culture;
	}
}
