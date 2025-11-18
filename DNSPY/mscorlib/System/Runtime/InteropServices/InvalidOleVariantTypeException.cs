using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200094F RID: 2383
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class InvalidOleVariantTypeException : SystemException
	{
		// Token: 0x060060C4 RID: 24772 RVA: 0x0014C8B1 File Offset: 0x0014AAB1
		[__DynamicallyInvokable]
		public InvalidOleVariantTypeException()
			: base(Environment.GetResourceString("Arg_InvalidOleVariantTypeException"))
		{
			base.SetErrorCode(-2146233039);
		}

		// Token: 0x060060C5 RID: 24773 RVA: 0x0014C8CE File Offset: 0x0014AACE
		[__DynamicallyInvokable]
		public InvalidOleVariantTypeException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233039);
		}

		// Token: 0x060060C6 RID: 24774 RVA: 0x0014C8E2 File Offset: 0x0014AAE2
		[__DynamicallyInvokable]
		public InvalidOleVariantTypeException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233039);
		}

		// Token: 0x060060C7 RID: 24775 RVA: 0x0014C8F7 File Offset: 0x0014AAF7
		protected InvalidOleVariantTypeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
