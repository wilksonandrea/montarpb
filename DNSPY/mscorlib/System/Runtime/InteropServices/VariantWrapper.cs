using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000963 RID: 2403
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class VariantWrapper
	{
		// Token: 0x06006228 RID: 25128 RVA: 0x0014F57A File Offset: 0x0014D77A
		[__DynamicallyInvokable]
		public VariantWrapper(object obj)
		{
			this.m_WrappedObject = obj;
		}

		// Token: 0x17001115 RID: 4373
		// (get) Token: 0x06006229 RID: 25129 RVA: 0x0014F589 File Offset: 0x0014D789
		[__DynamicallyInvokable]
		public object WrappedObject
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x04002B98 RID: 11160
		private object m_WrappedObject;
	}
}
