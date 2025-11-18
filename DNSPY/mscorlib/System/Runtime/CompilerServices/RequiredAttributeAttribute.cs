using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008C4 RID: 2244
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class RequiredAttributeAttribute : Attribute
	{
		// Token: 0x06005DC1 RID: 24001 RVA: 0x001496A4 File Offset: 0x001478A4
		public RequiredAttributeAttribute(Type requiredContract)
		{
			this.requiredContract = requiredContract;
		}

		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x06005DC2 RID: 24002 RVA: 0x001496B3 File Offset: 0x001478B3
		public Type RequiredContract
		{
			get
			{
				return this.requiredContract;
			}
		}

		// Token: 0x04002A33 RID: 10803
		private Type requiredContract;
	}
}
