using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000090 RID: 144
	[ComVisible(true)]
	[Serializable]
	public class ApplicationException : Exception
	{
		// Token: 0x0600076E RID: 1902 RVA: 0x0001A023 File Offset: 0x00018223
		public ApplicationException()
			: base(Environment.GetResourceString("Arg_ApplicationException"))
		{
			base.SetErrorCode(-2146232832);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001A040 File Offset: 0x00018240
		public ApplicationException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146232832);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001A054 File Offset: 0x00018254
		public ApplicationException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146232832);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0001A069 File Offset: 0x00018269
		protected ApplicationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
