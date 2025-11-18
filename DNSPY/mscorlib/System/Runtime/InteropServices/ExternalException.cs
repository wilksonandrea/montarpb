using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000948 RID: 2376
	[ComVisible(true)]
	[Serializable]
	public class ExternalException : SystemException
	{
		// Token: 0x0600607F RID: 24703 RVA: 0x0014BFF8 File Offset: 0x0014A1F8
		public ExternalException()
			: base(Environment.GetResourceString("Arg_ExternalException"))
		{
			base.SetErrorCode(-2147467259);
		}

		// Token: 0x06006080 RID: 24704 RVA: 0x0014C015 File Offset: 0x0014A215
		public ExternalException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147467259);
		}

		// Token: 0x06006081 RID: 24705 RVA: 0x0014C029 File Offset: 0x0014A229
		public ExternalException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2147467259);
		}

		// Token: 0x06006082 RID: 24706 RVA: 0x0014C03E File Offset: 0x0014A23E
		public ExternalException(string message, int errorCode)
			: base(message)
		{
			base.SetErrorCode(errorCode);
		}

		// Token: 0x06006083 RID: 24707 RVA: 0x0014C04E File Offset: 0x0014A24E
		protected ExternalException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x06006084 RID: 24708 RVA: 0x0014C058 File Offset: 0x0014A258
		public virtual int ErrorCode
		{
			get
			{
				return base.HResult;
			}
		}

		// Token: 0x06006085 RID: 24709 RVA: 0x0014C060 File Offset: 0x0014A260
		public override string ToString()
		{
			string message = this.Message;
			string text = base.GetType().ToString();
			string text2 = text + " (0x" + base.HResult.ToString("X8", CultureInfo.InvariantCulture) + ")";
			if (!string.IsNullOrEmpty(message))
			{
				text2 = text2 + ": " + message;
			}
			Exception innerException = base.InnerException;
			if (innerException != null)
			{
				text2 = text2 + " ---> " + innerException.ToString();
			}
			if (this.StackTrace != null)
			{
				text2 = text2 + Environment.NewLine + this.StackTrace;
			}
			return text2;
		}
	}
}
