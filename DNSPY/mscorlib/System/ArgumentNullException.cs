using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	// Token: 0x020000A7 RID: 167
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class ArgumentNullException : ArgumentException
	{
		// Token: 0x060009A3 RID: 2467 RVA: 0x0001F524 File Offset: 0x0001D724
		[__DynamicallyInvokable]
		public ArgumentNullException()
			: base(Environment.GetResourceString("ArgumentNull_Generic"))
		{
			base.SetErrorCode(-2147467261);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0001F541 File Offset: 0x0001D741
		[__DynamicallyInvokable]
		public ArgumentNullException(string paramName)
			: base(Environment.GetResourceString("ArgumentNull_Generic"), paramName)
		{
			base.SetErrorCode(-2147467261);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0001F55F File Offset: 0x0001D75F
		[__DynamicallyInvokable]
		public ArgumentNullException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147467261);
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0001F574 File Offset: 0x0001D774
		[__DynamicallyInvokable]
		public ArgumentNullException(string paramName, string message)
			: base(message, paramName)
		{
			base.SetErrorCode(-2147467261);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0001F589 File Offset: 0x0001D789
		[SecurityCritical]
		protected ArgumentNullException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
