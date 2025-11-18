using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x020004DA RID: 1242
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class KeyNotFoundException : SystemException, ISerializable
	{
		// Token: 0x06003AE7 RID: 15079 RVA: 0x000DF820 File Offset: 0x000DDA20
		[__DynamicallyInvokable]
		public KeyNotFoundException()
			: base(Environment.GetResourceString("Arg_KeyNotFound"))
		{
			base.SetErrorCode(-2146232969);
		}

		// Token: 0x06003AE8 RID: 15080 RVA: 0x000DF83D File Offset: 0x000DDA3D
		[__DynamicallyInvokable]
		public KeyNotFoundException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146232969);
		}

		// Token: 0x06003AE9 RID: 15081 RVA: 0x000DF851 File Offset: 0x000DDA51
		[__DynamicallyInvokable]
		public KeyNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146232969);
		}

		// Token: 0x06003AEA RID: 15082 RVA: 0x000DF866 File Offset: 0x000DDA66
		protected KeyNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
