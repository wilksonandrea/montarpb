using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000D8 RID: 216
	[ComVisible(true)]
	[Serializable]
	public class DuplicateWaitObjectException : ArgumentException
	{
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x0002A94F File Offset: 0x00028B4F
		private static string DuplicateWaitObjectMessage
		{
			get
			{
				if (DuplicateWaitObjectException._duplicateWaitObjectMessage == null)
				{
					DuplicateWaitObjectException._duplicateWaitObjectMessage = Environment.GetResourceString("Arg_DuplicateWaitObjectException");
				}
				return DuplicateWaitObjectException._duplicateWaitObjectMessage;
			}
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0002A972 File Offset: 0x00028B72
		public DuplicateWaitObjectException()
			: base(DuplicateWaitObjectException.DuplicateWaitObjectMessage)
		{
			base.SetErrorCode(-2146233047);
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0002A98A File Offset: 0x00028B8A
		public DuplicateWaitObjectException(string parameterName)
			: base(DuplicateWaitObjectException.DuplicateWaitObjectMessage, parameterName)
		{
			base.SetErrorCode(-2146233047);
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0002A9A3 File Offset: 0x00028BA3
		public DuplicateWaitObjectException(string parameterName, string message)
			: base(message, parameterName)
		{
			base.SetErrorCode(-2146233047);
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0002A9B8 File Offset: 0x00028BB8
		public DuplicateWaitObjectException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233047);
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0002A9CD File Offset: 0x00028BCD
		protected DuplicateWaitObjectException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000567 RID: 1383
		private static volatile string _duplicateWaitObjectMessage;
	}
}
