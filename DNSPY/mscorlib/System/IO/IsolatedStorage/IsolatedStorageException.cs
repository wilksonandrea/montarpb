using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO.IsolatedStorage
{
	// Token: 0x020001B1 RID: 433
	[ComVisible(true)]
	[Serializable]
	public class IsolatedStorageException : Exception
	{
		// Token: 0x06001B4F RID: 6991 RVA: 0x0005C6D6 File Offset: 0x0005A8D6
		public IsolatedStorageException()
			: base(Environment.GetResourceString("IsolatedStorage_Exception"))
		{
			base.SetErrorCode(-2146233264);
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x0005C6F3 File Offset: 0x0005A8F3
		public IsolatedStorageException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233264);
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x0005C707 File Offset: 0x0005A907
		public IsolatedStorageException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233264);
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x0005C71C File Offset: 0x0005A91C
		protected IsolatedStorageException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
