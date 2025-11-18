using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000957 RID: 2391
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class SEHException : ExternalException
	{
		// Token: 0x060061C4 RID: 25028 RVA: 0x0014E48C File Offset: 0x0014C68C
		[__DynamicallyInvokable]
		public SEHException()
		{
			base.SetErrorCode(-2147467259);
		}

		// Token: 0x060061C5 RID: 25029 RVA: 0x0014E49F File Offset: 0x0014C69F
		[__DynamicallyInvokable]
		public SEHException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147467259);
		}

		// Token: 0x060061C6 RID: 25030 RVA: 0x0014E4B3 File Offset: 0x0014C6B3
		[__DynamicallyInvokable]
		public SEHException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2147467259);
		}

		// Token: 0x060061C7 RID: 25031 RVA: 0x0014E4C8 File Offset: 0x0014C6C8
		protected SEHException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060061C8 RID: 25032 RVA: 0x0014E4D2 File Offset: 0x0014C6D2
		[__DynamicallyInvokable]
		public virtual bool CanResume()
		{
			return false;
		}
	}
}
