using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000A2 RID: 162
	[ComVisible(true)]
	[Serializable]
	public class AppDomainUnloadedException : SystemException
	{
		// Token: 0x06000965 RID: 2405 RVA: 0x0001E9FD File Offset: 0x0001CBFD
		public AppDomainUnloadedException()
			: base(Environment.GetResourceString("Arg_AppDomainUnloadedException"))
		{
			base.SetErrorCode(-2146234348);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0001EA1A File Offset: 0x0001CC1A
		public AppDomainUnloadedException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146234348);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0001EA2E File Offset: 0x0001CC2E
		public AppDomainUnloadedException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146234348);
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0001EA43 File Offset: 0x0001CC43
		protected AppDomainUnloadedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
