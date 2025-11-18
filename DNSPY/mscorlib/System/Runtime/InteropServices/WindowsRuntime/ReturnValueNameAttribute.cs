using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009CD RID: 2509
	[AttributeUsage(AttributeTargets.Delegate | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class ReturnValueNameAttribute : Attribute
	{
		// Token: 0x060063D0 RID: 25552 RVA: 0x00154759 File Offset: 0x00152959
		[__DynamicallyInvokable]
		public ReturnValueNameAttribute(string name)
		{
			this.m_Name = name;
		}

		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x060063D1 RID: 25553 RVA: 0x00154768 File Offset: 0x00152968
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x04002CE8 RID: 11496
		private string m_Name;
	}
}
