using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x02000512 RID: 1298
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class SynchronizationLockException : SystemException
	{
		// Token: 0x06003D17 RID: 15639 RVA: 0x000E5EE6 File Offset: 0x000E40E6
		[__DynamicallyInvokable]
		public SynchronizationLockException()
			: base(Environment.GetResourceString("Arg_SynchronizationLockException"))
		{
			base.SetErrorCode(-2146233064);
		}

		// Token: 0x06003D18 RID: 15640 RVA: 0x000E5F03 File Offset: 0x000E4103
		[__DynamicallyInvokable]
		public SynchronizationLockException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233064);
		}

		// Token: 0x06003D19 RID: 15641 RVA: 0x000E5F17 File Offset: 0x000E4117
		[__DynamicallyInvokable]
		public SynchronizationLockException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233064);
		}

		// Token: 0x06003D1A RID: 15642 RVA: 0x000E5F2C File Offset: 0x000E412C
		protected SynchronizationLockException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
