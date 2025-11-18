using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x0200052A RID: 1322
	[ComVisible(true)]
	[Serializable]
	public class ThreadStateException : SystemException
	{
		// Token: 0x06003E2C RID: 15916 RVA: 0x000E79CC File Offset: 0x000E5BCC
		public ThreadStateException()
			: base(Environment.GetResourceString("Arg_ThreadStateException"))
		{
			base.SetErrorCode(-2146233056);
		}

		// Token: 0x06003E2D RID: 15917 RVA: 0x000E79E9 File Offset: 0x000E5BE9
		public ThreadStateException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233056);
		}

		// Token: 0x06003E2E RID: 15918 RVA: 0x000E79FD File Offset: 0x000E5BFD
		public ThreadStateException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233056);
		}

		// Token: 0x06003E2F RID: 15919 RVA: 0x000E7A12 File Offset: 0x000E5C12
		protected ThreadStateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
