using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x02000511 RID: 1297
	[ComVisible(false)]
	[TypeForwardedFrom("System, Version=2.0.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
	[__DynamicallyInvokable]
	[Serializable]
	public class SemaphoreFullException : SystemException
	{
		// Token: 0x06003D13 RID: 15635 RVA: 0x000E5EB7 File Offset: 0x000E40B7
		[__DynamicallyInvokable]
		public SemaphoreFullException()
			: base(Environment.GetResourceString("Threading_SemaphoreFullException"))
		{
		}

		// Token: 0x06003D14 RID: 15636 RVA: 0x000E5EC9 File Offset: 0x000E40C9
		[__DynamicallyInvokable]
		public SemaphoreFullException(string message)
			: base(message)
		{
		}

		// Token: 0x06003D15 RID: 15637 RVA: 0x000E5ED2 File Offset: 0x000E40D2
		[__DynamicallyInvokable]
		public SemaphoreFullException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06003D16 RID: 15638 RVA: 0x000E5EDC File Offset: 0x000E40DC
		protected SemaphoreFullException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
