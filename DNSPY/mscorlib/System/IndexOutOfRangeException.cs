using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000F1 RID: 241
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class IndexOutOfRangeException : SystemException
	{
		// Token: 0x06000F01 RID: 3841 RVA: 0x0002EE1D File Offset: 0x0002D01D
		[__DynamicallyInvokable]
		public IndexOutOfRangeException()
			: base(Environment.GetResourceString("Arg_IndexOutOfRangeException"))
		{
			base.SetErrorCode(-2146233080);
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0002EE3A File Offset: 0x0002D03A
		[__DynamicallyInvokable]
		public IndexOutOfRangeException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233080);
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x0002EE4E File Offset: 0x0002D04E
		[__DynamicallyInvokable]
		public IndexOutOfRangeException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233080);
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0002EE63 File Offset: 0x0002D063
		internal IndexOutOfRangeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
