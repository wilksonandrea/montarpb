using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x02000625 RID: 1573
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class TargetParameterCountException : ApplicationException
	{
		// Token: 0x060048C2 RID: 18626 RVA: 0x001077AB File Offset: 0x001059AB
		[__DynamicallyInvokable]
		public TargetParameterCountException()
			: base(Environment.GetResourceString("Arg_TargetParameterCountException"))
		{
			base.SetErrorCode(-2147352562);
		}

		// Token: 0x060048C3 RID: 18627 RVA: 0x001077C8 File Offset: 0x001059C8
		[__DynamicallyInvokable]
		public TargetParameterCountException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147352562);
		}

		// Token: 0x060048C4 RID: 18628 RVA: 0x001077DC File Offset: 0x001059DC
		[__DynamicallyInvokable]
		public TargetParameterCountException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2147352562);
		}

		// Token: 0x060048C5 RID: 18629 RVA: 0x001077F1 File Offset: 0x001059F1
		internal TargetParameterCountException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
