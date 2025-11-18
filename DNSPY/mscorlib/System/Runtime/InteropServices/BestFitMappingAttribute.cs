using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000940 RID: 2368
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class BestFitMappingAttribute : Attribute
	{
		// Token: 0x06006063 RID: 24675 RVA: 0x0014BDB5 File Offset: 0x00149FB5
		[__DynamicallyInvokable]
		public BestFitMappingAttribute(bool BestFitMapping)
		{
			this._bestFitMapping = BestFitMapping;
		}

		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x06006064 RID: 24676 RVA: 0x0014BDC4 File Offset: 0x00149FC4
		[__DynamicallyInvokable]
		public bool BestFitMapping
		{
			[__DynamicallyInvokable]
			get
			{
				return this._bestFitMapping;
			}
		}

		// Token: 0x04002B36 RID: 11062
		internal bool _bestFitMapping;

		// Token: 0x04002B37 RID: 11063
		[__DynamicallyInvokable]
		public bool ThrowOnUnmappableChar;
	}
}
