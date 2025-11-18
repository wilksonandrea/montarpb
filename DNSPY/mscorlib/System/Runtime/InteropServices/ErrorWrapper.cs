using System;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000961 RID: 2401
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class ErrorWrapper
	{
		// Token: 0x06006222 RID: 25122 RVA: 0x0014F507 File Offset: 0x0014D707
		[__DynamicallyInvokable]
		public ErrorWrapper(int errorCode)
		{
			this.m_ErrorCode = errorCode;
		}

		// Token: 0x06006223 RID: 25123 RVA: 0x0014F516 File Offset: 0x0014D716
		[__DynamicallyInvokable]
		public ErrorWrapper(object errorCode)
		{
			if (!(errorCode is int))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeInt32"), "errorCode");
			}
			this.m_ErrorCode = (int)errorCode;
		}

		// Token: 0x06006224 RID: 25124 RVA: 0x0014F547 File Offset: 0x0014D747
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public ErrorWrapper(Exception e)
		{
			this.m_ErrorCode = Marshal.GetHRForException(e);
		}

		// Token: 0x17001113 RID: 4371
		// (get) Token: 0x06006225 RID: 25125 RVA: 0x0014F55B File Offset: 0x0014D75B
		[__DynamicallyInvokable]
		public int ErrorCode
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_ErrorCode;
			}
		}

		// Token: 0x04002B96 RID: 11158
		private int m_ErrorCode;
	}
}
