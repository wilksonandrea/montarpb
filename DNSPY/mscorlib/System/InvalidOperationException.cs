using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000100 RID: 256
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class InvalidOperationException : SystemException
	{
		// Token: 0x06000FB3 RID: 4019 RVA: 0x000302CD File Offset: 0x0002E4CD
		[__DynamicallyInvokable]
		public InvalidOperationException()
			: base(Environment.GetResourceString("Arg_InvalidOperationException"))
		{
			base.SetErrorCode(-2146233079);
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x000302EA File Offset: 0x0002E4EA
		[__DynamicallyInvokable]
		public InvalidOperationException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233079);
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x000302FE File Offset: 0x0002E4FE
		[__DynamicallyInvokable]
		public InvalidOperationException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233079);
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x00030313 File Offset: 0x0002E513
		protected InvalidOperationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
