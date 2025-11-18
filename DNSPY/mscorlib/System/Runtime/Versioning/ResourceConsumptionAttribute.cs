using System;
using System.Diagnostics;

namespace System.Runtime.Versioning
{
	// Token: 0x02000721 RID: 1825
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, Inherited = false)]
	[Conditional("RESOURCE_ANNOTATION_WORK")]
	public sealed class ResourceConsumptionAttribute : Attribute
	{
		// Token: 0x0600515D RID: 20829 RVA: 0x0011EB1F File Offset: 0x0011CD1F
		public ResourceConsumptionAttribute(ResourceScope resourceScope)
		{
			this._resourceScope = resourceScope;
			this._consumptionScope = this._resourceScope;
		}

		// Token: 0x0600515E RID: 20830 RVA: 0x0011EB3A File Offset: 0x0011CD3A
		public ResourceConsumptionAttribute(ResourceScope resourceScope, ResourceScope consumptionScope)
		{
			this._resourceScope = resourceScope;
			this._consumptionScope = consumptionScope;
		}

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x0600515F RID: 20831 RVA: 0x0011EB50 File Offset: 0x0011CD50
		public ResourceScope ResourceScope
		{
			get
			{
				return this._resourceScope;
			}
		}

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x06005160 RID: 20832 RVA: 0x0011EB58 File Offset: 0x0011CD58
		public ResourceScope ConsumptionScope
		{
			get
			{
				return this._consumptionScope;
			}
		}

		// Token: 0x04002417 RID: 9239
		private ResourceScope _consumptionScope;

		// Token: 0x04002418 RID: 9240
		private ResourceScope _resourceScope;
	}
}
