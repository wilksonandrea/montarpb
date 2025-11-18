using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000518 RID: 1304
	[ComVisible(true)]
	[Serializable]
	public sealed class ThreadAbortException : SystemException
	{
		// Token: 0x06003DBC RID: 15804 RVA: 0x000E6B7F File Offset: 0x000E4D7F
		internal ThreadAbortException()
			: base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.ThreadAbort))
		{
			base.SetErrorCode(-2146233040);
		}

		// Token: 0x06003DBD RID: 15805 RVA: 0x000E6B98 File Offset: 0x000E4D98
		internal ThreadAbortException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06003DBE RID: 15806 RVA: 0x000E6BA2 File Offset: 0x000E4DA2
		public object ExceptionState
		{
			[SecuritySafeCritical]
			get
			{
				return Thread.CurrentThread.AbortReason;
			}
		}
	}
}
