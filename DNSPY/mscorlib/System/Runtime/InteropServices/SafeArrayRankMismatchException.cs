using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000979 RID: 2425
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class SafeArrayRankMismatchException : SystemException
	{
		// Token: 0x06006265 RID: 25189 RVA: 0x00150DBE File Offset: 0x0014EFBE
		[__DynamicallyInvokable]
		public SafeArrayRankMismatchException()
			: base(Environment.GetResourceString("Arg_SafeArrayRankMismatchException"))
		{
			base.SetErrorCode(-2146233032);
		}

		// Token: 0x06006266 RID: 25190 RVA: 0x00150DDB File Offset: 0x0014EFDB
		[__DynamicallyInvokable]
		public SafeArrayRankMismatchException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233032);
		}

		// Token: 0x06006267 RID: 25191 RVA: 0x00150DEF File Offset: 0x0014EFEF
		[__DynamicallyInvokable]
		public SafeArrayRankMismatchException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233032);
		}

		// Token: 0x06006268 RID: 25192 RVA: 0x00150E04 File Offset: 0x0014F004
		protected SafeArrayRankMismatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
