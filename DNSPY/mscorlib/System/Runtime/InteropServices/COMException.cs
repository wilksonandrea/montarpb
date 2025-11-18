using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security;
using Microsoft.Win32;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000946 RID: 2374
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class COMException : ExternalException
	{
		// Token: 0x0600606B RID: 24683 RVA: 0x0014BE11 File Offset: 0x0014A011
		[__DynamicallyInvokable]
		public COMException()
			: base(Environment.GetResourceString("Arg_COMException"))
		{
			base.SetErrorCode(-2147467259);
		}

		// Token: 0x0600606C RID: 24684 RVA: 0x0014BE2E File Offset: 0x0014A02E
		[__DynamicallyInvokable]
		public COMException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147467259);
		}

		// Token: 0x0600606D RID: 24685 RVA: 0x0014BE42 File Offset: 0x0014A042
		[__DynamicallyInvokable]
		public COMException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2147467259);
		}

		// Token: 0x0600606E RID: 24686 RVA: 0x0014BE57 File Offset: 0x0014A057
		[__DynamicallyInvokable]
		public COMException(string message, int errorCode)
			: base(message)
		{
			base.SetErrorCode(errorCode);
		}

		// Token: 0x0600606F RID: 24687 RVA: 0x0014BE67 File Offset: 0x0014A067
		[SecuritySafeCritical]
		internal COMException(int hresult)
			: base(Win32Native.GetMessage(hresult))
		{
			base.SetErrorCode(hresult);
		}

		// Token: 0x06006070 RID: 24688 RVA: 0x0014BE7C File Offset: 0x0014A07C
		internal COMException(string message, int hresult, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(hresult);
		}

		// Token: 0x06006071 RID: 24689 RVA: 0x0014BE8D File Offset: 0x0014A08D
		protected COMException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06006072 RID: 24690 RVA: 0x0014BE98 File Offset: 0x0014A098
		public override string ToString()
		{
			string message = this.Message;
			string text = base.GetType().ToString();
			string text2 = text + " (0x" + base.HResult.ToString("X8", CultureInfo.InvariantCulture) + ")";
			if (message != null && message.Length > 0)
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
