using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005B8 RID: 1464
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyProductAttribute : Attribute
	{
		// Token: 0x0600445C RID: 17500 RVA: 0x000FC1C6 File Offset: 0x000FA3C6
		[__DynamicallyInvokable]
		public AssemblyProductAttribute(string product)
		{
			this.m_product = product;
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x0600445D RID: 17501 RVA: 0x000FC1D5 File Offset: 0x000FA3D5
		[__DynamicallyInvokable]
		public string Product
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_product;
			}
		}

		// Token: 0x04001C07 RID: 7175
		private string m_product;
	}
}
