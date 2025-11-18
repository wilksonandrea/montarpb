using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000AA RID: 170
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class ArithmeticException : SystemException
	{
		// Token: 0x060009C0 RID: 2496 RVA: 0x0001F7AE File Offset: 0x0001D9AE
		[__DynamicallyInvokable]
		public ArithmeticException()
			: base(Environment.GetResourceString("Arg_ArithmeticException"))
		{
			base.SetErrorCode(-2147024362);
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0001F7CB File Offset: 0x0001D9CB
		[__DynamicallyInvokable]
		public ArithmeticException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024362);
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0001F7DF File Offset: 0x0001D9DF
		[__DynamicallyInvokable]
		public ArithmeticException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147024362);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0001F7F4 File Offset: 0x0001D9F4
		protected ArithmeticException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
