using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005C2 RID: 1474
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyKeyFileAttribute : Attribute
	{
		// Token: 0x06004470 RID: 17520 RVA: 0x000FC2BA File Offset: 0x000FA4BA
		[__DynamicallyInvokable]
		public AssemblyKeyFileAttribute(string keyFile)
		{
			this.m_keyFile = keyFile;
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06004471 RID: 17521 RVA: 0x000FC2C9 File Offset: 0x000FA4C9
		[__DynamicallyInvokable]
		public string KeyFile
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_keyFile;
			}
		}

		// Token: 0x04001C11 RID: 7185
		private string m_keyFile;
	}
}
