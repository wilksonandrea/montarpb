using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000154 RID: 340
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class UnauthorizedAccessException : SystemException
	{
		// Token: 0x06001559 RID: 5465 RVA: 0x0003E67E File Offset: 0x0003C87E
		[__DynamicallyInvokable]
		public UnauthorizedAccessException()
			: base(Environment.GetResourceString("Arg_UnauthorizedAccessException"))
		{
			base.SetErrorCode(-2147024891);
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x0003E69B File Offset: 0x0003C89B
		[__DynamicallyInvokable]
		public UnauthorizedAccessException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024891);
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x0003E6AF File Offset: 0x0003C8AF
		[__DynamicallyInvokable]
		public UnauthorizedAccessException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2147024891);
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x0003E6C4 File Offset: 0x0003C8C4
		protected UnauthorizedAccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
