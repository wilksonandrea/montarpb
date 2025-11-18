using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000917 RID: 2327
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class ComDefaultInterfaceAttribute : Attribute
	{
		// Token: 0x06006000 RID: 24576 RVA: 0x0014B4AC File Offset: 0x001496AC
		[__DynamicallyInvokable]
		public ComDefaultInterfaceAttribute(Type defaultInterface)
		{
			this._val = defaultInterface;
		}

		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x06006001 RID: 24577 RVA: 0x0014B4BB File Offset: 0x001496BB
		[__DynamicallyInvokable]
		public Type Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A73 RID: 10867
		internal Type _val;
	}
}
