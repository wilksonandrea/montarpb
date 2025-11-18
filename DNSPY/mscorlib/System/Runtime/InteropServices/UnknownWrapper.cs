using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000962 RID: 2402
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class UnknownWrapper
	{
		// Token: 0x06006226 RID: 25126 RVA: 0x0014F563 File Offset: 0x0014D763
		[__DynamicallyInvokable]
		public UnknownWrapper(object obj)
		{
			this.m_WrappedObject = obj;
		}

		// Token: 0x17001114 RID: 4372
		// (get) Token: 0x06006227 RID: 25127 RVA: 0x0014F572 File Offset: 0x0014D772
		[__DynamicallyInvokable]
		public object WrappedObject
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x04002B97 RID: 11159
		private object m_WrappedObject;
	}
}
