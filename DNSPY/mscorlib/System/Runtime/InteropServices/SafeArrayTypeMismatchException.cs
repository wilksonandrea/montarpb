using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200097A RID: 2426
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class SafeArrayTypeMismatchException : SystemException
	{
		// Token: 0x06006269 RID: 25193 RVA: 0x00150E0E File Offset: 0x0014F00E
		[__DynamicallyInvokable]
		public SafeArrayTypeMismatchException()
			: base(Environment.GetResourceString("Arg_SafeArrayTypeMismatchException"))
		{
			base.SetErrorCode(-2146233037);
		}

		// Token: 0x0600626A RID: 25194 RVA: 0x00150E2B File Offset: 0x0014F02B
		[__DynamicallyInvokable]
		public SafeArrayTypeMismatchException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233037);
		}

		// Token: 0x0600626B RID: 25195 RVA: 0x00150E3F File Offset: 0x0014F03F
		[__DynamicallyInvokable]
		public SafeArrayTypeMismatchException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233037);
		}

		// Token: 0x0600626C RID: 25196 RVA: 0x00150E54 File Offset: 0x0014F054
		protected SafeArrayTypeMismatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
