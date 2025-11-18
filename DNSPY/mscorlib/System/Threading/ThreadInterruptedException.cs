using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x02000519 RID: 1305
	[ComVisible(true)]
	[Serializable]
	public class ThreadInterruptedException : SystemException
	{
		// Token: 0x06003DBF RID: 15807 RVA: 0x000E6BAE File Offset: 0x000E4DAE
		public ThreadInterruptedException()
			: base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.ThreadInterrupted))
		{
			base.SetErrorCode(-2146233063);
		}

		// Token: 0x06003DC0 RID: 15808 RVA: 0x000E6BC7 File Offset: 0x000E4DC7
		public ThreadInterruptedException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233063);
		}

		// Token: 0x06003DC1 RID: 15809 RVA: 0x000E6BDB File Offset: 0x000E4DDB
		public ThreadInterruptedException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233063);
		}

		// Token: 0x06003DC2 RID: 15810 RVA: 0x000E6BF0 File Offset: 0x000E4DF0
		protected ThreadInterruptedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
