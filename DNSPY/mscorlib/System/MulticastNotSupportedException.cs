using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000112 RID: 274
	[ComVisible(true)]
	[Serializable]
	public sealed class MulticastNotSupportedException : SystemException
	{
		// Token: 0x0600106F RID: 4207 RVA: 0x0003150C File Offset: 0x0002F70C
		public MulticastNotSupportedException()
			: base(Environment.GetResourceString("Arg_MulticastNotSupportedException"))
		{
			base.SetErrorCode(-2146233068);
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x00031529 File Offset: 0x0002F729
		public MulticastNotSupportedException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233068);
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x0003153D File Offset: 0x0002F73D
		public MulticastNotSupportedException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233068);
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x00031552 File Offset: 0x0002F752
		internal MulticastNotSupportedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
