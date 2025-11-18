using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000127 RID: 295
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class RankException : SystemException
	{
		// Token: 0x06001102 RID: 4354 RVA: 0x00033335 File Offset: 0x00031535
		[__DynamicallyInvokable]
		public RankException()
			: base(Environment.GetResourceString("Arg_RankException"))
		{
			base.SetErrorCode(-2146233065);
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00033352 File Offset: 0x00031552
		[__DynamicallyInvokable]
		public RankException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233065);
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00033366 File Offset: 0x00031566
		[__DynamicallyInvokable]
		public RankException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233065);
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x0003337B File Offset: 0x0003157B
		protected RankException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
