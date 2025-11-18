using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
	// Token: 0x020007CA RID: 1994
	[ComVisible(true)]
	[Serializable]
	public class ServerException : SystemException
	{
		// Token: 0x0600561E RID: 22046 RVA: 0x00131401 File Offset: 0x0012F601
		public ServerException()
			: base(ServerException._nullMessage)
		{
			base.SetErrorCode(-2146233074);
		}

		// Token: 0x0600561F RID: 22047 RVA: 0x00131419 File Offset: 0x0012F619
		public ServerException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233074);
		}

		// Token: 0x06005620 RID: 22048 RVA: 0x0013142D File Offset: 0x0012F62D
		public ServerException(string message, Exception InnerException)
			: base(message, InnerException)
		{
			base.SetErrorCode(-2146233074);
		}

		// Token: 0x06005621 RID: 22049 RVA: 0x00131442 File Offset: 0x0012F642
		internal ServerException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06005622 RID: 22050 RVA: 0x0013144C File Offset: 0x0012F64C
		// Note: this type is marked as 'beforefieldinit'.
		static ServerException()
		{
		}

		// Token: 0x04002797 RID: 10135
		private static string _nullMessage = Environment.GetResourceString("Remoting_Default");
	}
}
