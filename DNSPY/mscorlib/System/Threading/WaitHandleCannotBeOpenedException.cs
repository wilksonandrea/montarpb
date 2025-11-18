using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x02000535 RID: 1333
	[ComVisible(false)]
	[__DynamicallyInvokable]
	[Serializable]
	public class WaitHandleCannotBeOpenedException : ApplicationException
	{
		// Token: 0x06003EAB RID: 16043 RVA: 0x000E906D File Offset: 0x000E726D
		[__DynamicallyInvokable]
		public WaitHandleCannotBeOpenedException()
			: base(Environment.GetResourceString("Threading.WaitHandleCannotBeOpenedException"))
		{
			base.SetErrorCode(-2146233044);
		}

		// Token: 0x06003EAC RID: 16044 RVA: 0x000E908A File Offset: 0x000E728A
		[__DynamicallyInvokable]
		public WaitHandleCannotBeOpenedException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233044);
		}

		// Token: 0x06003EAD RID: 16045 RVA: 0x000E909E File Offset: 0x000E729E
		[__DynamicallyInvokable]
		public WaitHandleCannotBeOpenedException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233044);
		}

		// Token: 0x06003EAE RID: 16046 RVA: 0x000E90B3 File Offset: 0x000E72B3
		protected WaitHandleCannotBeOpenedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
