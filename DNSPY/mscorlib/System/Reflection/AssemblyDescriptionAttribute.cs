using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005BA RID: 1466
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyDescriptionAttribute : Attribute
	{
		// Token: 0x06004460 RID: 17504 RVA: 0x000FC1F4 File Offset: 0x000FA3F4
		[__DynamicallyInvokable]
		public AssemblyDescriptionAttribute(string description)
		{
			this.m_description = description;
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06004461 RID: 17505 RVA: 0x000FC203 File Offset: 0x000FA403
		[__DynamicallyInvokable]
		public string Description
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_description;
			}
		}

		// Token: 0x04001C09 RID: 7177
		private string m_description;
	}
}
