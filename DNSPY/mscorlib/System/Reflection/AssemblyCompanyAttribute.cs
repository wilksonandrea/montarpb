using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005B9 RID: 1465
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyCompanyAttribute : Attribute
	{
		// Token: 0x0600445E RID: 17502 RVA: 0x000FC1DD File Offset: 0x000FA3DD
		[__DynamicallyInvokable]
		public AssemblyCompanyAttribute(string company)
		{
			this.m_company = company;
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x0600445F RID: 17503 RVA: 0x000FC1EC File Offset: 0x000FA3EC
		[__DynamicallyInvokable]
		public string Company
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_company;
			}
		}

		// Token: 0x04001C08 RID: 7176
		private string m_company;
	}
}
