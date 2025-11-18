using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000911 RID: 2321
	[AttributeUsage(AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class UnmanagedFunctionPointerAttribute : Attribute
	{
		// Token: 0x06005FF4 RID: 24564 RVA: 0x0014B422 File Offset: 0x00149622
		[__DynamicallyInvokable]
		public UnmanagedFunctionPointerAttribute(CallingConvention callingConvention)
		{
			this.m_callingConvention = callingConvention;
		}

		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x06005FF5 RID: 24565 RVA: 0x0014B431 File Offset: 0x00149631
		[__DynamicallyInvokable]
		public CallingConvention CallingConvention
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_callingConvention;
			}
		}

		// Token: 0x04002A65 RID: 10853
		private CallingConvention m_callingConvention;

		// Token: 0x04002A66 RID: 10854
		[__DynamicallyInvokable]
		public CharSet CharSet;

		// Token: 0x04002A67 RID: 10855
		[__DynamicallyInvokable]
		public bool BestFitMapping;

		// Token: 0x04002A68 RID: 10856
		[__DynamicallyInvokable]
		public bool ThrowOnUnmappableChar;

		// Token: 0x04002A69 RID: 10857
		[__DynamicallyInvokable]
		public bool SetLastError;
	}
}
