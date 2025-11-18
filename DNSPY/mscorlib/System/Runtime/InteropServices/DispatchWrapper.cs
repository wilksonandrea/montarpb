using System;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000960 RID: 2400
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class DispatchWrapper
	{
		// Token: 0x06006220 RID: 25120 RVA: 0x0014F4D4 File Offset: 0x0014D6D4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public DispatchWrapper(object obj)
		{
			if (obj != null)
			{
				IntPtr idispatchForObject = Marshal.GetIDispatchForObject(obj);
				Marshal.Release(idispatchForObject);
			}
			this.m_WrappedObject = obj;
		}

		// Token: 0x17001112 RID: 4370
		// (get) Token: 0x06006221 RID: 25121 RVA: 0x0014F4FF File Offset: 0x0014D6FF
		[__DynamicallyInvokable]
		public object WrappedObject
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x04002B95 RID: 11157
		private object m_WrappedObject;
	}
}
