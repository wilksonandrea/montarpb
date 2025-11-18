using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x02000180 RID: 384
	[ComVisible(true)]
	[Serializable]
	public class DriveNotFoundException : IOException
	{
		// Token: 0x06001795 RID: 6037 RVA: 0x0004BBB1 File Offset: 0x00049DB1
		public DriveNotFoundException()
			: base(Environment.GetResourceString("Arg_DriveNotFoundException"))
		{
			base.SetErrorCode(-2147024893);
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x0004BBCE File Offset: 0x00049DCE
		public DriveNotFoundException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024893);
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x0004BBE2 File Offset: 0x00049DE2
		public DriveNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147024893);
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x0004BBF7 File Offset: 0x00049DF7
		protected DriveNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
