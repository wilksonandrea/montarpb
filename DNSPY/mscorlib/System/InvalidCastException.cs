using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000FF RID: 255
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class InvalidCastException : SystemException
	{
		// Token: 0x06000FAE RID: 4014 RVA: 0x0003026D File Offset: 0x0002E46D
		[__DynamicallyInvokable]
		public InvalidCastException()
			: base(Environment.GetResourceString("Arg_InvalidCastException"))
		{
			base.SetErrorCode(-2147467262);
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0003028A File Offset: 0x0002E48A
		[__DynamicallyInvokable]
		public InvalidCastException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147467262);
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x0003029E File Offset: 0x0002E49E
		[__DynamicallyInvokable]
		public InvalidCastException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147467262);
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x000302B3 File Offset: 0x0002E4B3
		protected InvalidCastException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x000302BD File Offset: 0x0002E4BD
		[__DynamicallyInvokable]
		public InvalidCastException(string message, int errorCode)
			: base(message)
		{
			base.SetErrorCode(errorCode);
		}
	}
}
