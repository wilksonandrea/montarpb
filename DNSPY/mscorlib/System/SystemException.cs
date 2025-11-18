using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000080 RID: 128
	[ComVisible(true)]
	[Serializable]
	public class SystemException : Exception
	{
		// Token: 0x060006C4 RID: 1732 RVA: 0x0001791E File Offset: 0x00015B1E
		public SystemException()
			: base(Environment.GetResourceString("Arg_SystemException"))
		{
			base.SetErrorCode(-2146233087);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001793B File Offset: 0x00015B3B
		public SystemException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233087);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001794F File Offset: 0x00015B4F
		public SystemException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233087);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00017964 File Offset: 0x00015B64
		protected SystemException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
