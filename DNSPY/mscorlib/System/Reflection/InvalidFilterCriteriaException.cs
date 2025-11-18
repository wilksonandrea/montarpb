using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020005F1 RID: 1521
	[ComVisible(true)]
	[Serializable]
	public class InvalidFilterCriteriaException : ApplicationException
	{
		// Token: 0x06004667 RID: 18023 RVA: 0x0010247D File Offset: 0x0010067D
		public InvalidFilterCriteriaException()
			: base(Environment.GetResourceString("Arg_InvalidFilterCriteriaException"))
		{
			base.SetErrorCode(-2146232831);
		}

		// Token: 0x06004668 RID: 18024 RVA: 0x0010249A File Offset: 0x0010069A
		public InvalidFilterCriteriaException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146232831);
		}

		// Token: 0x06004669 RID: 18025 RVA: 0x001024AE File Offset: 0x001006AE
		public InvalidFilterCriteriaException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146232831);
		}

		// Token: 0x0600466A RID: 18026 RVA: 0x001024C3 File Offset: 0x001006C3
		protected InvalidFilterCriteriaException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
