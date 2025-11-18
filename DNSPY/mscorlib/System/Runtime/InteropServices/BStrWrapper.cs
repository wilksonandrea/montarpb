using System;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200095E RID: 2398
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class BStrWrapper
	{
		// Token: 0x0600621A RID: 25114 RVA: 0x0014F45F File Offset: 0x0014D65F
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public BStrWrapper(string value)
		{
			this.m_WrappedObject = value;
		}

		// Token: 0x0600621B RID: 25115 RVA: 0x0014F46E File Offset: 0x0014D66E
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public BStrWrapper(object value)
		{
			this.m_WrappedObject = (string)value;
		}

		// Token: 0x17001110 RID: 4368
		// (get) Token: 0x0600621C RID: 25116 RVA: 0x0014F482 File Offset: 0x0014D682
		[__DynamicallyInvokable]
		public string WrappedObject
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x04002B93 RID: 11155
		private string m_WrappedObject;
	}
}
