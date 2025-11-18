using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000F5 RID: 245
	[Serializable]
	public sealed class InsufficientMemoryException : OutOfMemoryException
	{
		// Token: 0x06000F0A RID: 3850 RVA: 0x0002EE6D File Offset: 0x0002D06D
		public InsufficientMemoryException()
			: base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.OutOfMemory))
		{
			base.SetErrorCode(-2146233027);
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0002EE86 File Offset: 0x0002D086
		public InsufficientMemoryException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233027);
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0002EE9A File Offset: 0x0002D09A
		public InsufficientMemoryException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233027);
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0002EEAF File Offset: 0x0002D0AF
		private InsufficientMemoryException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
