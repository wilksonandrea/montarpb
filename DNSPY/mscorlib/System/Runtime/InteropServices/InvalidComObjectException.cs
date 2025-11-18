using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200096A RID: 2410
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class InvalidComObjectException : SystemException
	{
		// Token: 0x0600622F RID: 25135 RVA: 0x0014F599 File Offset: 0x0014D799
		[__DynamicallyInvokable]
		public InvalidComObjectException()
			: base(Environment.GetResourceString("Arg_InvalidComObjectException"))
		{
			base.SetErrorCode(-2146233049);
		}

		// Token: 0x06006230 RID: 25136 RVA: 0x0014F5B6 File Offset: 0x0014D7B6
		[__DynamicallyInvokable]
		public InvalidComObjectException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233049);
		}

		// Token: 0x06006231 RID: 25137 RVA: 0x0014F5CA File Offset: 0x0014D7CA
		[__DynamicallyInvokable]
		public InvalidComObjectException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233049);
		}

		// Token: 0x06006232 RID: 25138 RVA: 0x0014F5DF File Offset: 0x0014D7DF
		protected InvalidComObjectException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
