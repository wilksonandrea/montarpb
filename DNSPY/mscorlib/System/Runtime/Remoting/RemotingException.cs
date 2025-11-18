using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
	// Token: 0x020007C9 RID: 1993
	[ComVisible(true)]
	[Serializable]
	public class RemotingException : SystemException
	{
		// Token: 0x06005619 RID: 22041 RVA: 0x001313A5 File Offset: 0x0012F5A5
		public RemotingException()
			: base(RemotingException._nullMessage)
		{
			base.SetErrorCode(-2146233077);
		}

		// Token: 0x0600561A RID: 22042 RVA: 0x001313BD File Offset: 0x0012F5BD
		public RemotingException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233077);
		}

		// Token: 0x0600561B RID: 22043 RVA: 0x001313D1 File Offset: 0x0012F5D1
		public RemotingException(string message, Exception InnerException)
			: base(message, InnerException)
		{
			base.SetErrorCode(-2146233077);
		}

		// Token: 0x0600561C RID: 22044 RVA: 0x001313E6 File Offset: 0x0012F5E6
		protected RemotingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600561D RID: 22045 RVA: 0x001313F0 File Offset: 0x0012F5F0
		// Note: this type is marked as 'beforefieldinit'.
		static RemotingException()
		{
		}

		// Token: 0x04002796 RID: 10134
		private static string _nullMessage = Environment.GetResourceString("Remoting_Default");
	}
}
