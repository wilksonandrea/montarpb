using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005C8 RID: 1480
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyKeyNameAttribute : Attribute
	{
		// Token: 0x06004482 RID: 17538 RVA: 0x000FC397 File Offset: 0x000FA597
		[__DynamicallyInvokable]
		public AssemblyKeyNameAttribute(string keyName)
		{
			this.m_keyName = keyName;
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06004483 RID: 17539 RVA: 0x000FC3A6 File Offset: 0x000FA5A6
		[__DynamicallyInvokable]
		public string KeyName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_keyName;
			}
		}

		// Token: 0x04001C19 RID: 7193
		private string m_keyName;
	}
}
