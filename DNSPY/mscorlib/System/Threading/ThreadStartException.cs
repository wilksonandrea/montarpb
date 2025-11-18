using System;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x0200052B RID: 1323
	[Serializable]
	public sealed class ThreadStartException : SystemException
	{
		// Token: 0x06003E30 RID: 15920 RVA: 0x000E7A1C File Offset: 0x000E5C1C
		private ThreadStartException()
			: base(Environment.GetResourceString("Arg_ThreadStartException"))
		{
			base.SetErrorCode(-2146233051);
		}

		// Token: 0x06003E31 RID: 15921 RVA: 0x000E7A39 File Offset: 0x000E5C39
		private ThreadStartException(Exception reason)
			: base(Environment.GetResourceString("Arg_ThreadStartException"), reason)
		{
			base.SetErrorCode(-2146233051);
		}

		// Token: 0x06003E32 RID: 15922 RVA: 0x000E7A57 File Offset: 0x000E5C57
		internal ThreadStartException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
