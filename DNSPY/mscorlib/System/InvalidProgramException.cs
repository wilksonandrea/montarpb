using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000101 RID: 257
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class InvalidProgramException : SystemException
	{
		// Token: 0x06000FB7 RID: 4023 RVA: 0x0003031D File Offset: 0x0002E51D
		[__DynamicallyInvokable]
		public InvalidProgramException()
			: base(Environment.GetResourceString("InvalidProgram_Default"))
		{
			base.SetErrorCode(-2146233030);
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0003033A File Offset: 0x0002E53A
		[__DynamicallyInvokable]
		public InvalidProgramException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233030);
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0003034E File Offset: 0x0002E54E
		[__DynamicallyInvokable]
		public InvalidProgramException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233030);
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x00030363 File Offset: 0x0002E563
		internal InvalidProgramException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
