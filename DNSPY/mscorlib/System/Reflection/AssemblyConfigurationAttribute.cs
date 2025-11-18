using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005BC RID: 1468
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyConfigurationAttribute : Attribute
	{
		// Token: 0x06004464 RID: 17508 RVA: 0x000FC222 File Offset: 0x000FA422
		[__DynamicallyInvokable]
		public AssemblyConfigurationAttribute(string configuration)
		{
			this.m_configuration = configuration;
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06004465 RID: 17509 RVA: 0x000FC231 File Offset: 0x000FA431
		[__DynamicallyInvokable]
		public string Configuration
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_configuration;
			}
		}

		// Token: 0x04001C0B RID: 7179
		private string m_configuration;
	}
}
