using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200093D RID: 2365
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class ComEventInterfaceAttribute : Attribute
	{
		// Token: 0x06006058 RID: 24664 RVA: 0x0014BD24 File Offset: 0x00149F24
		[__DynamicallyInvokable]
		public ComEventInterfaceAttribute(Type SourceInterface, Type EventProvider)
		{
			this._SourceInterface = SourceInterface;
			this._EventProvider = EventProvider;
		}

		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x06006059 RID: 24665 RVA: 0x0014BD3A File Offset: 0x00149F3A
		[__DynamicallyInvokable]
		public Type SourceInterface
		{
			[__DynamicallyInvokable]
			get
			{
				return this._SourceInterface;
			}
		}

		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x0600605A RID: 24666 RVA: 0x0014BD42 File Offset: 0x00149F42
		[__DynamicallyInvokable]
		public Type EventProvider
		{
			[__DynamicallyInvokable]
			get
			{
				return this._EventProvider;
			}
		}

		// Token: 0x04002B2E RID: 11054
		internal Type _SourceInterface;

		// Token: 0x04002B2F RID: 11055
		internal Type _EventProvider;
	}
}
