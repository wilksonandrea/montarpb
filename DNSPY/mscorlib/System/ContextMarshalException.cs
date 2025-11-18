using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000CA RID: 202
	[ComVisible(true)]
	[Serializable]
	public class ContextMarshalException : SystemException
	{
		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002515B File Offset: 0x0002335B
		public ContextMarshalException()
			: base(Environment.GetResourceString("Arg_ContextMarshalException"))
		{
			base.SetErrorCode(-2146233084);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00025178 File Offset: 0x00023378
		public ContextMarshalException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233084);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002518C File Offset: 0x0002338C
		public ContextMarshalException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233084);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x000251A1 File Offset: 0x000233A1
		protected ContextMarshalException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
