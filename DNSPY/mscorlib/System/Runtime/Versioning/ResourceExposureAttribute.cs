using System;
using System.Diagnostics;

namespace System.Runtime.Versioning
{
	// Token: 0x02000722 RID: 1826
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, Inherited = false)]
	[Conditional("RESOURCE_ANNOTATION_WORK")]
	public sealed class ResourceExposureAttribute : Attribute
	{
		// Token: 0x06005161 RID: 20833 RVA: 0x0011EB60 File Offset: 0x0011CD60
		public ResourceExposureAttribute(ResourceScope exposureLevel)
		{
			this._resourceExposureLevel = exposureLevel;
		}

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x06005162 RID: 20834 RVA: 0x0011EB6F File Offset: 0x0011CD6F
		public ResourceScope ResourceExposureLevel
		{
			get
			{
				return this._resourceExposureLevel;
			}
		}

		// Token: 0x04002419 RID: 9241
		private ResourceScope _resourceExposureLevel;
	}
}
