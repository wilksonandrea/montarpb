using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000DB RID: 219
	[ComVisible(true)]
	[Serializable]
	public class EntryPointNotFoundException : TypeLoadException
	{
		// Token: 0x06000E29 RID: 3625 RVA: 0x0002BCD7 File Offset: 0x00029ED7
		public EntryPointNotFoundException()
			: base(Environment.GetResourceString("Arg_EntryPointNotFoundException"))
		{
			base.SetErrorCode(-2146233053);
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0002BCF4 File Offset: 0x00029EF4
		public EntryPointNotFoundException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233053);
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0002BD08 File Offset: 0x00029F08
		public EntryPointNotFoundException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233053);
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0002BD1D File Offset: 0x00029F1D
		protected EntryPointNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
