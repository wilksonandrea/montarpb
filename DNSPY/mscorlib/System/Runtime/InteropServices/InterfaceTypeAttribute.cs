using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000916 RID: 2326
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class InterfaceTypeAttribute : Attribute
	{
		// Token: 0x06005FFD RID: 24573 RVA: 0x0014B486 File Offset: 0x00149686
		[__DynamicallyInvokable]
		public InterfaceTypeAttribute(ComInterfaceType interfaceType)
		{
			this._val = interfaceType;
		}

		// Token: 0x06005FFE RID: 24574 RVA: 0x0014B495 File Offset: 0x00149695
		[__DynamicallyInvokable]
		public InterfaceTypeAttribute(short interfaceType)
		{
			this._val = (ComInterfaceType)interfaceType;
		}

		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x06005FFF RID: 24575 RVA: 0x0014B4A4 File Offset: 0x001496A4
		[__DynamicallyInvokable]
		public ComInterfaceType Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A72 RID: 10866
		internal ComInterfaceType _val;
	}
}
