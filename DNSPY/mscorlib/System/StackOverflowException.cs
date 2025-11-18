using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000082 RID: 130
	[ComVisible(true)]
	[Serializable]
	public sealed class StackOverflowException : SystemException
	{
		// Token: 0x060006CC RID: 1740 RVA: 0x000179BA File Offset: 0x00015BBA
		public StackOverflowException()
			: base(Environment.GetResourceString("Arg_StackOverflowException"))
		{
			base.SetErrorCode(-2147023895);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x000179D7 File Offset: 0x00015BD7
		public StackOverflowException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147023895);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x000179EB File Offset: 0x00015BEB
		public StackOverflowException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147023895);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00017A00 File Offset: 0x00015C00
		internal StackOverflowException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
