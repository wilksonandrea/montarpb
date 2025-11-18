using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200008F RID: 143
	[ComVisible(true)]
	[Serializable]
	public class AccessViolationException : SystemException
	{
		// Token: 0x0600076A RID: 1898 RVA: 0x00019FD3 File Offset: 0x000181D3
		public AccessViolationException()
			: base(Environment.GetResourceString("Arg_AccessViolationException"))
		{
			base.SetErrorCode(-2147467261);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00019FF0 File Offset: 0x000181F0
		public AccessViolationException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147467261);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0001A004 File Offset: 0x00018204
		public AccessViolationException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147467261);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0001A019 File Offset: 0x00018219
		protected AccessViolationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000377 RID: 887
		private IntPtr _ip;

		// Token: 0x04000378 RID: 888
		private IntPtr _target;

		// Token: 0x04000379 RID: 889
		private int _accessType;
	}
}
