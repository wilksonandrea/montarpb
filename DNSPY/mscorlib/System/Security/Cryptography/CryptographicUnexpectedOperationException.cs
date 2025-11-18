using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Cryptography
{
	// Token: 0x02000244 RID: 580
	[ComVisible(true)]
	[Serializable]
	public class CryptographicUnexpectedOperationException : CryptographicException
	{
		// Token: 0x060020B5 RID: 8373 RVA: 0x000726F0 File Offset: 0x000708F0
		public CryptographicUnexpectedOperationException()
		{
			base.SetErrorCode(-2146233295);
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x00072703 File Offset: 0x00070903
		public CryptographicUnexpectedOperationException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233295);
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x00072717 File Offset: 0x00070917
		public CryptographicUnexpectedOperationException(string format, string insert)
			: base(string.Format(CultureInfo.CurrentCulture, format, insert))
		{
			base.SetErrorCode(-2146233295);
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x00072736 File Offset: 0x00070936
		public CryptographicUnexpectedOperationException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233295);
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x0007274B File Offset: 0x0007094B
		protected CryptographicUnexpectedOperationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
