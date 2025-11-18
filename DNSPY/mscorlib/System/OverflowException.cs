using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200011E RID: 286
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class OverflowException : ArithmeticException
	{
		// Token: 0x060010D7 RID: 4311 RVA: 0x00032D2B File Offset: 0x00030F2B
		[__DynamicallyInvokable]
		public OverflowException()
			: base(Environment.GetResourceString("Arg_OverflowException"))
		{
			base.SetErrorCode(-2146233066);
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00032D48 File Offset: 0x00030F48
		[__DynamicallyInvokable]
		public OverflowException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233066);
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00032D5C File Offset: 0x00030F5C
		[__DynamicallyInvokable]
		public OverflowException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233066);
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x00032D71 File Offset: 0x00030F71
		protected OverflowException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
