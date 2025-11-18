using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000919 RID: 2329
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class ClassInterfaceAttribute : Attribute
	{
		// Token: 0x06006002 RID: 24578 RVA: 0x0014B4C3 File Offset: 0x001496C3
		[__DynamicallyInvokable]
		public ClassInterfaceAttribute(ClassInterfaceType classInterfaceType)
		{
			this._val = classInterfaceType;
		}

		// Token: 0x06006003 RID: 24579 RVA: 0x0014B4D2 File Offset: 0x001496D2
		[__DynamicallyInvokable]
		public ClassInterfaceAttribute(short classInterfaceType)
		{
			this._val = (ClassInterfaceType)classInterfaceType;
		}

		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x06006004 RID: 24580 RVA: 0x0014B4E1 File Offset: 0x001496E1
		[__DynamicallyInvokable]
		public ClassInterfaceType Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A78 RID: 10872
		internal ClassInterfaceType _val;
	}
}
