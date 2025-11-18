using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000F6 RID: 246
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class InsufficientExecutionStackException : SystemException
	{
		// Token: 0x06000F0E RID: 3854 RVA: 0x0002EEB9 File Offset: 0x0002D0B9
		[__DynamicallyInvokable]
		public InsufficientExecutionStackException()
			: base(Environment.GetResourceString("Arg_InsufficientExecutionStackException"))
		{
			base.SetErrorCode(-2146232968);
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0002EED6 File Offset: 0x0002D0D6
		[__DynamicallyInvokable]
		public InsufficientExecutionStackException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146232968);
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0002EEEA File Offset: 0x0002D0EA
		[__DynamicallyInvokable]
		public InsufficientExecutionStackException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146232968);
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0002EEFF File Offset: 0x0002D0FF
		private InsufficientExecutionStackException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
