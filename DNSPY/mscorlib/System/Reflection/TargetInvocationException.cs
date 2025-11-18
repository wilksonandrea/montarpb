using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x02000624 RID: 1572
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class TargetInvocationException : ApplicationException
	{
		// Token: 0x060048BD RID: 18621 RVA: 0x0010773D File Offset: 0x0010593D
		private TargetInvocationException()
			: base(Environment.GetResourceString("Arg_TargetInvocationException"))
		{
			base.SetErrorCode(-2146232828);
		}

		// Token: 0x060048BE RID: 18622 RVA: 0x0010775A File Offset: 0x0010595A
		private TargetInvocationException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146232828);
		}

		// Token: 0x060048BF RID: 18623 RVA: 0x0010776E File Offset: 0x0010596E
		[__DynamicallyInvokable]
		public TargetInvocationException(Exception inner)
			: base(Environment.GetResourceString("Arg_TargetInvocationException"), inner)
		{
			base.SetErrorCode(-2146232828);
		}

		// Token: 0x060048C0 RID: 18624 RVA: 0x0010778C File Offset: 0x0010598C
		[__DynamicallyInvokable]
		public TargetInvocationException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146232828);
		}

		// Token: 0x060048C1 RID: 18625 RVA: 0x001077A1 File Offset: 0x001059A1
		internal TargetInvocationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
