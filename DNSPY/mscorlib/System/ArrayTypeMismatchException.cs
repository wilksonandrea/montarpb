using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000AB RID: 171
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class ArrayTypeMismatchException : SystemException
	{
		// Token: 0x060009C4 RID: 2500 RVA: 0x0001F7FE File Offset: 0x0001D9FE
		[__DynamicallyInvokable]
		public ArrayTypeMismatchException()
			: base(Environment.GetResourceString("Arg_ArrayTypeMismatchException"))
		{
			base.SetErrorCode(-2146233085);
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0001F81B File Offset: 0x0001DA1B
		[__DynamicallyInvokable]
		public ArrayTypeMismatchException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233085);
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0001F82F File Offset: 0x0001DA2F
		[__DynamicallyInvokable]
		public ArrayTypeMismatchException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233085);
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0001F844 File Offset: 0x0001DA44
		protected ArrayTypeMismatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
