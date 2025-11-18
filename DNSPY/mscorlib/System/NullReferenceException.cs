using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000117 RID: 279
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class NullReferenceException : SystemException
	{
		// Token: 0x06001087 RID: 4231 RVA: 0x0003173D File Offset: 0x0002F93D
		[__DynamicallyInvokable]
		public NullReferenceException()
			: base(Environment.GetResourceString("Arg_NullReferenceException"))
		{
			base.SetErrorCode(-2147467261);
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x0003175A File Offset: 0x0002F95A
		[__DynamicallyInvokable]
		public NullReferenceException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147467261);
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0003176E File Offset: 0x0002F96E
		[__DynamicallyInvokable]
		public NullReferenceException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147467261);
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00031783 File Offset: 0x0002F983
		protected NullReferenceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
