using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000142 RID: 322
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class TimeoutException : SystemException
	{
		// Token: 0x06001342 RID: 4930 RVA: 0x00038886 File Offset: 0x00036A86
		[__DynamicallyInvokable]
		public TimeoutException()
			: base(Environment.GetResourceString("Arg_TimeoutException"))
		{
			base.SetErrorCode(-2146233083);
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x000388A3 File Offset: 0x00036AA3
		[__DynamicallyInvokable]
		public TimeoutException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233083);
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x000388B7 File Offset: 0x00036AB7
		[__DynamicallyInvokable]
		public TimeoutException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233083);
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x000388CC File Offset: 0x00036ACC
		protected TimeoutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
