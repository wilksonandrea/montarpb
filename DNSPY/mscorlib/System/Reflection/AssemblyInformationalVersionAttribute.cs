using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005BE RID: 1470
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyInformationalVersionAttribute : Attribute
	{
		// Token: 0x06004468 RID: 17512 RVA: 0x000FC250 File Offset: 0x000FA450
		[__DynamicallyInvokable]
		public AssemblyInformationalVersionAttribute(string informationalVersion)
		{
			this.m_informationalVersion = informationalVersion;
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06004469 RID: 17513 RVA: 0x000FC25F File Offset: 0x000FA45F
		[__DynamicallyInvokable]
		public string InformationalVersion
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_informationalVersion;
			}
		}

		// Token: 0x04001C0D RID: 7181
		private string m_informationalVersion;
	}
}
