using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200008D RID: 141
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class MemberAccessException : SystemException
	{
		// Token: 0x06000743 RID: 1859 RVA: 0x00019960 File Offset: 0x00017B60
		[__DynamicallyInvokable]
		public MemberAccessException()
			: base(Environment.GetResourceString("Arg_AccessException"))
		{
			base.SetErrorCode(-2146233062);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001997D File Offset: 0x00017B7D
		[__DynamicallyInvokable]
		public MemberAccessException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233062);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00019991 File Offset: 0x00017B91
		[__DynamicallyInvokable]
		public MemberAccessException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233062);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x000199A6 File Offset: 0x00017BA6
		protected MemberAccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
