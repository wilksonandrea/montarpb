using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200095F RID: 2399
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class CurrencyWrapper
	{
		// Token: 0x0600621D RID: 25117 RVA: 0x0014F48A File Offset: 0x0014D68A
		[__DynamicallyInvokable]
		public CurrencyWrapper(decimal obj)
		{
			this.m_WrappedObject = obj;
		}

		// Token: 0x0600621E RID: 25118 RVA: 0x0014F499 File Offset: 0x0014D699
		[__DynamicallyInvokable]
		public CurrencyWrapper(object obj)
		{
			if (!(obj is decimal))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDecimal"), "obj");
			}
			this.m_WrappedObject = (decimal)obj;
		}

		// Token: 0x17001111 RID: 4369
		// (get) Token: 0x0600621F RID: 25119 RVA: 0x0014F4CA File Offset: 0x0014D6CA
		[__DynamicallyInvokable]
		public decimal WrappedObject
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x04002B94 RID: 11156
		private decimal m_WrappedObject;
	}
}
