using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000954 RID: 2388
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class MarshalDirectiveException : SystemException
	{
		// Token: 0x060061B3 RID: 25011 RVA: 0x0014E354 File Offset: 0x0014C554
		[__DynamicallyInvokable]
		public MarshalDirectiveException()
			: base(Environment.GetResourceString("Arg_MarshalDirectiveException"))
		{
			base.SetErrorCode(-2146233035);
		}

		// Token: 0x060061B4 RID: 25012 RVA: 0x0014E371 File Offset: 0x0014C571
		[__DynamicallyInvokable]
		public MarshalDirectiveException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233035);
		}

		// Token: 0x060061B5 RID: 25013 RVA: 0x0014E385 File Offset: 0x0014C585
		[__DynamicallyInvokable]
		public MarshalDirectiveException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233035);
		}

		// Token: 0x060061B6 RID: 25014 RVA: 0x0014E39A File Offset: 0x0014C59A
		protected MarshalDirectiveException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
