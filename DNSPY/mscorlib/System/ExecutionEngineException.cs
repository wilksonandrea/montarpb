using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000084 RID: 132
	[Obsolete("This type previously indicated an unspecified fatal error in the runtime. The runtime no longer raises this exception so this type is obsolete.")]
	[ComVisible(true)]
	[Serializable]
	public sealed class ExecutionEngineException : SystemException
	{
		// Token: 0x060006D4 RID: 1748 RVA: 0x00017A5A File Offset: 0x00015C5A
		public ExecutionEngineException()
			: base(Environment.GetResourceString("Arg_ExecutionEngineException"))
		{
			base.SetErrorCode(-2146233082);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00017A77 File Offset: 0x00015C77
		public ExecutionEngineException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233082);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00017A8B File Offset: 0x00015C8B
		public ExecutionEngineException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233082);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00017AA0 File Offset: 0x00015CA0
		internal ExecutionEngineException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
