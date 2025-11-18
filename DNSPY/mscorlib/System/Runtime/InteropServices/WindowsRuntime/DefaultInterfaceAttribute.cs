using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009C8 RID: 2504
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class DefaultInterfaceAttribute : Attribute
	{
		// Token: 0x060063C5 RID: 25541 RVA: 0x001546D5 File Offset: 0x001528D5
		[__DynamicallyInvokable]
		public DefaultInterfaceAttribute(Type defaultInterface)
		{
			this.m_defaultInterface = defaultInterface;
		}

		// Token: 0x17001139 RID: 4409
		// (get) Token: 0x060063C6 RID: 25542 RVA: 0x001546E4 File Offset: 0x001528E4
		[__DynamicallyInvokable]
		public Type DefaultInterface
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_defaultInterface;
			}
		}

		// Token: 0x04002CE2 RID: 11490
		private Type m_defaultInterface;
	}
}
