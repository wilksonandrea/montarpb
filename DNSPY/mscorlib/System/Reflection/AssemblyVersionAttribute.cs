using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005C1 RID: 1473
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyVersionAttribute : Attribute
	{
		// Token: 0x0600446E RID: 17518 RVA: 0x000FC2A3 File Offset: 0x000FA4A3
		[__DynamicallyInvokable]
		public AssemblyVersionAttribute(string version)
		{
			this.m_version = version;
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x0600446F RID: 17519 RVA: 0x000FC2B2 File Offset: 0x000FA4B2
		[__DynamicallyInvokable]
		public string Version
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x04001C10 RID: 7184
		private string m_version;
	}
}
