using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security
{
	// Token: 0x020001F6 RID: 502
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class VerificationException : SystemException
	{
		// Token: 0x06001E3F RID: 7743 RVA: 0x000698BA File Offset: 0x00067ABA
		[__DynamicallyInvokable]
		public VerificationException()
			: base(Environment.GetResourceString("Verification_Exception"))
		{
			base.SetErrorCode(-2146233075);
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x000698D7 File Offset: 0x00067AD7
		[__DynamicallyInvokable]
		public VerificationException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233075);
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x000698EB File Offset: 0x00067AEB
		[__DynamicallyInvokable]
		public VerificationException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233075);
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x00069900 File Offset: 0x00067B00
		protected VerificationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
