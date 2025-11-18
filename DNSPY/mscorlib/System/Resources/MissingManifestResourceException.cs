using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Resources
{
	// Token: 0x02000391 RID: 913
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class MissingManifestResourceException : SystemException
	{
		// Token: 0x06002D07 RID: 11527 RVA: 0x000A9F9B File Offset: 0x000A819B
		[__DynamicallyInvokable]
		public MissingManifestResourceException()
			: base(Environment.GetResourceString("Arg_MissingManifestResourceException"))
		{
			base.SetErrorCode(-2146233038);
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x000A9FB8 File Offset: 0x000A81B8
		[__DynamicallyInvokable]
		public MissingManifestResourceException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233038);
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x000A9FCC File Offset: 0x000A81CC
		[__DynamicallyInvokable]
		public MissingManifestResourceException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233038);
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x000A9FE1 File Offset: 0x000A81E1
		protected MissingManifestResourceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
