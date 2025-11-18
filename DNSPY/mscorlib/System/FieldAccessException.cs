using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000E2 RID: 226
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class FieldAccessException : MemberAccessException
	{
		// Token: 0x06000E7F RID: 3711 RVA: 0x0002CD2F File Offset: 0x0002AF2F
		[__DynamicallyInvokable]
		public FieldAccessException()
			: base(Environment.GetResourceString("Arg_FieldAccessException"))
		{
			base.SetErrorCode(-2146233081);
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0002CD4C File Offset: 0x0002AF4C
		[__DynamicallyInvokable]
		public FieldAccessException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233081);
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0002CD60 File Offset: 0x0002AF60
		[__DynamicallyInvokable]
		public FieldAccessException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233081);
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0002CD75 File Offset: 0x0002AF75
		protected FieldAccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
