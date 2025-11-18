using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x02000181 RID: 385
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class EndOfStreamException : IOException
	{
		// Token: 0x06001799 RID: 6041 RVA: 0x0004BC01 File Offset: 0x00049E01
		[__DynamicallyInvokable]
		public EndOfStreamException()
			: base(Environment.GetResourceString("Arg_EndOfStreamException"))
		{
			base.SetErrorCode(-2147024858);
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x0004BC1E File Offset: 0x00049E1E
		[__DynamicallyInvokable]
		public EndOfStreamException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024858);
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x0004BC32 File Offset: 0x00049E32
		[__DynamicallyInvokable]
		public EndOfStreamException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147024858);
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x0004BC47 File Offset: 0x00049E47
		protected EndOfStreamException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
