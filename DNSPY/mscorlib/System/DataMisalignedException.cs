using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000083 RID: 131
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class DataMisalignedException : SystemException
	{
		// Token: 0x060006D0 RID: 1744 RVA: 0x00017A0A File Offset: 0x00015C0A
		[__DynamicallyInvokable]
		public DataMisalignedException()
			: base(Environment.GetResourceString("Arg_DataMisalignedException"))
		{
			base.SetErrorCode(-2146233023);
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00017A27 File Offset: 0x00015C27
		[__DynamicallyInvokable]
		public DataMisalignedException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233023);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00017A3B File Offset: 0x00015C3B
		[__DynamicallyInvokable]
		public DataMisalignedException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233023);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00017A50 File Offset: 0x00015C50
		internal DataMisalignedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
