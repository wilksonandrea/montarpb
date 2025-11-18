using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000123 RID: 291
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class PlatformNotSupportedException : NotSupportedException
	{
		// Token: 0x060010EC RID: 4332 RVA: 0x00032EE3 File Offset: 0x000310E3
		[__DynamicallyInvokable]
		public PlatformNotSupportedException()
			: base(Environment.GetResourceString("Arg_PlatformNotSupported"))
		{
			base.SetErrorCode(-2146233031);
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x00032F00 File Offset: 0x00031100
		[__DynamicallyInvokable]
		public PlatformNotSupportedException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233031);
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00032F14 File Offset: 0x00031114
		[__DynamicallyInvokable]
		public PlatformNotSupportedException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233031);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x00032F29 File Offset: 0x00031129
		protected PlatformNotSupportedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
