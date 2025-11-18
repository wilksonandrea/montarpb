using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020005B1 RID: 1457
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class AmbiguousMatchException : SystemException
	{
		// Token: 0x06004371 RID: 17265 RVA: 0x000FA585 File Offset: 0x000F8785
		[__DynamicallyInvokable]
		public AmbiguousMatchException()
			: base(Environment.GetResourceString("RFLCT.Ambiguous"))
		{
			base.SetErrorCode(-2147475171);
		}

		// Token: 0x06004372 RID: 17266 RVA: 0x000FA5A2 File Offset: 0x000F87A2
		[__DynamicallyInvokable]
		public AmbiguousMatchException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147475171);
		}

		// Token: 0x06004373 RID: 17267 RVA: 0x000FA5B6 File Offset: 0x000F87B6
		[__DynamicallyInvokable]
		public AmbiguousMatchException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2147475171);
		}

		// Token: 0x06004374 RID: 17268 RVA: 0x000FA5CB File Offset: 0x000F87CB
		internal AmbiguousMatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
