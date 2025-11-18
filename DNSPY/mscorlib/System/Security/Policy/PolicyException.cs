using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Policy
{
	// Token: 0x02000363 RID: 867
	[ComVisible(true)]
	[Serializable]
	public class PolicyException : SystemException
	{
		// Token: 0x06002AD6 RID: 10966 RVA: 0x0009E6B7 File Offset: 0x0009C8B7
		public PolicyException()
			: base(Environment.GetResourceString("Policy_Default"))
		{
			base.HResult = -2146233322;
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x0009E6D4 File Offset: 0x0009C8D4
		public PolicyException(string message)
			: base(message)
		{
			base.HResult = -2146233322;
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x0009E6E8 File Offset: 0x0009C8E8
		public PolicyException(string message, Exception exception)
			: base(message, exception)
		{
			base.HResult = -2146233322;
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x0009E6FD File Offset: 0x0009C8FD
		protected PolicyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x0009E707 File Offset: 0x0009C907
		internal PolicyException(string message, int hresult)
			: base(message)
		{
			base.HResult = hresult;
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x0009E717 File Offset: 0x0009C917
		internal PolicyException(string message, int hresult, Exception exception)
			: base(message, exception)
		{
			base.HResult = hresult;
		}
	}
}
