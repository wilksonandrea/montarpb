using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008E5 RID: 2277
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class RuntimeCompatibilityAttribute : Attribute
	{
		// Token: 0x06005DE7 RID: 24039 RVA: 0x00149871 File Offset: 0x00147A71
		[__DynamicallyInvokable]
		public RuntimeCompatibilityAttribute()
		{
		}

		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x06005DE8 RID: 24040 RVA: 0x00149879 File Offset: 0x00147A79
		// (set) Token: 0x06005DE9 RID: 24041 RVA: 0x00149881 File Offset: 0x00147A81
		[__DynamicallyInvokable]
		public bool WrapNonExceptionThrows
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_wrapNonExceptionThrows;
			}
			[__DynamicallyInvokable]
			set
			{
				this.m_wrapNonExceptionThrows = value;
			}
		}

		// Token: 0x04002A44 RID: 10820
		private bool m_wrapNonExceptionThrows;
	}
}
