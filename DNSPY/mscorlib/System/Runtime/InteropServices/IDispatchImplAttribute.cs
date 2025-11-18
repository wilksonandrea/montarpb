using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000922 RID: 2338
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, Inherited = false)]
	[Obsolete("This attribute is deprecated and will be removed in a future version.", false)]
	[ComVisible(true)]
	public sealed class IDispatchImplAttribute : Attribute
	{
		// Token: 0x06006011 RID: 24593 RVA: 0x0014B571 File Offset: 0x00149771
		public IDispatchImplAttribute(IDispatchImplType implType)
		{
			this._val = implType;
		}

		// Token: 0x06006012 RID: 24594 RVA: 0x0014B580 File Offset: 0x00149780
		public IDispatchImplAttribute(short implType)
		{
			this._val = (IDispatchImplType)implType;
		}

		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x06006013 RID: 24595 RVA: 0x0014B58F File Offset: 0x0014978F
		public IDispatchImplType Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A82 RID: 10882
		internal IDispatchImplType _val;
	}
}
