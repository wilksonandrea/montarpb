using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200093C RID: 2364
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class CoClassAttribute : Attribute
	{
		// Token: 0x06006056 RID: 24662 RVA: 0x0014BD0D File Offset: 0x00149F0D
		[__DynamicallyInvokable]
		public CoClassAttribute(Type coClass)
		{
			this._CoClass = coClass;
		}

		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x06006057 RID: 24663 RVA: 0x0014BD1C File Offset: 0x00149F1C
		[__DynamicallyInvokable]
		public Type CoClass
		{
			[__DynamicallyInvokable]
			get
			{
				return this._CoClass;
			}
		}

		// Token: 0x04002B2D RID: 11053
		internal Type _CoClass;
	}
}
