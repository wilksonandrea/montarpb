using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000081 RID: 129
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class OutOfMemoryException : SystemException
	{
		// Token: 0x060006C8 RID: 1736 RVA: 0x0001796E File Offset: 0x00015B6E
		[__DynamicallyInvokable]
		public OutOfMemoryException()
			: base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.OutOfMemory))
		{
			base.SetErrorCode(-2147024882);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00017987 File Offset: 0x00015B87
		[__DynamicallyInvokable]
		public OutOfMemoryException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024882);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0001799B File Offset: 0x00015B9B
		[__DynamicallyInvokable]
		public OutOfMemoryException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147024882);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x000179B0 File Offset: 0x00015BB0
		protected OutOfMemoryException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
