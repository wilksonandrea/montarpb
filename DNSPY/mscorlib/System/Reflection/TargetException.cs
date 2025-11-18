using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x02000623 RID: 1571
	[ComVisible(true)]
	[Serializable]
	public class TargetException : ApplicationException
	{
		// Token: 0x060048B9 RID: 18617 RVA: 0x001076F7 File Offset: 0x001058F7
		public TargetException()
		{
			base.SetErrorCode(-2146232829);
		}

		// Token: 0x060048BA RID: 18618 RVA: 0x0010770A File Offset: 0x0010590A
		public TargetException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146232829);
		}

		// Token: 0x060048BB RID: 18619 RVA: 0x0010771E File Offset: 0x0010591E
		public TargetException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146232829);
		}

		// Token: 0x060048BC RID: 18620 RVA: 0x00107733 File Offset: 0x00105933
		protected TargetException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
