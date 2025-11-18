using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000BF RID: 191
	[ComVisible(true)]
	[Serializable]
	public class TypeUnloadedException : SystemException
	{
		// Token: 0x06000AFF RID: 2815 RVA: 0x00022C62 File Offset: 0x00020E62
		public TypeUnloadedException()
			: base(Environment.GetResourceString("Arg_TypeUnloadedException"))
		{
			base.SetErrorCode(-2146234349);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00022C7F File Offset: 0x00020E7F
		public TypeUnloadedException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146234349);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00022C93 File Offset: 0x00020E93
		public TypeUnloadedException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146234349);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00022CA8 File Offset: 0x00020EA8
		protected TypeUnloadedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
