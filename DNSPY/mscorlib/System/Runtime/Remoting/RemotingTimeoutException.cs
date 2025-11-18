using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
	// Token: 0x020007CB RID: 1995
	[ComVisible(true)]
	[Serializable]
	public class RemotingTimeoutException : RemotingException
	{
		// Token: 0x06005623 RID: 22051 RVA: 0x0013145D File Offset: 0x0012F65D
		public RemotingTimeoutException()
			: base(RemotingTimeoutException._nullMessage)
		{
		}

		// Token: 0x06005624 RID: 22052 RVA: 0x0013146A File Offset: 0x0012F66A
		public RemotingTimeoutException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233077);
		}

		// Token: 0x06005625 RID: 22053 RVA: 0x0013147E File Offset: 0x0012F67E
		public RemotingTimeoutException(string message, Exception InnerException)
			: base(message, InnerException)
		{
			base.SetErrorCode(-2146233077);
		}

		// Token: 0x06005626 RID: 22054 RVA: 0x00131493 File Offset: 0x0012F693
		internal RemotingTimeoutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06005627 RID: 22055 RVA: 0x0013149D File Offset: 0x0012F69D
		// Note: this type is marked as 'beforefieldinit'.
		static RemotingTimeoutException()
		{
		}

		// Token: 0x04002798 RID: 10136
		private static string _nullMessage = Environment.GetResourceString("Remoting_Default");
	}
}
