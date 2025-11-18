using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000B5 RID: 181
	[ComVisible(true)]
	[Serializable]
	public class CannotUnloadAppDomainException : SystemException
	{
		// Token: 0x06000A84 RID: 2692 RVA: 0x000218B1 File Offset: 0x0001FAB1
		public CannotUnloadAppDomainException()
			: base(Environment.GetResourceString("Arg_CannotUnloadAppDomainException"))
		{
			base.SetErrorCode(-2146234347);
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x000218CE File Offset: 0x0001FACE
		public CannotUnloadAppDomainException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146234347);
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x000218E2 File Offset: 0x0001FAE2
		public CannotUnloadAppDomainException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146234347);
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x000218F7 File Offset: 0x0001FAF7
		protected CannotUnloadAppDomainException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
