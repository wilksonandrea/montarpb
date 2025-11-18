using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security
{
	// Token: 0x020001C0 RID: 448
	[ComVisible(true)]
	[Serializable]
	public sealed class XmlSyntaxException : SystemException
	{
		// Token: 0x06001C16 RID: 7190 RVA: 0x00060B1E File Offset: 0x0005ED1E
		public XmlSyntaxException()
			: base(Environment.GetResourceString("XMLSyntax_InvalidSyntax"))
		{
			base.SetErrorCode(-2146233320);
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x00060B3B File Offset: 0x0005ED3B
		public XmlSyntaxException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233320);
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x00060B4F File Offset: 0x0005ED4F
		public XmlSyntaxException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233320);
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x00060B64 File Offset: 0x0005ED64
		public XmlSyntaxException(int lineNumber)
			: base(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("XMLSyntax_SyntaxError"), lineNumber))
		{
			base.SetErrorCode(-2146233320);
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x00060B91 File Offset: 0x0005ED91
		public XmlSyntaxException(int lineNumber, string message)
			: base(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("XMLSyntax_SyntaxErrorEx"), lineNumber, message))
		{
			base.SetErrorCode(-2146233320);
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x00060BBF File Offset: 0x0005EDBF
		internal XmlSyntaxException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
