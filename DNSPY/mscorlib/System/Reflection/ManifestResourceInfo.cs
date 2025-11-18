using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005F5 RID: 1525
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public class ManifestResourceInfo
	{
		// Token: 0x0600467B RID: 18043 RVA: 0x00102553 File Offset: 0x00100753
		[__DynamicallyInvokable]
		public ManifestResourceInfo(Assembly containingAssembly, string containingFileName, ResourceLocation resourceLocation)
		{
			this._containingAssembly = containingAssembly;
			this._containingFileName = containingFileName;
			this._resourceLocation = resourceLocation;
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x0600467C RID: 18044 RVA: 0x00102570 File Offset: 0x00100770
		[__DynamicallyInvokable]
		public virtual Assembly ReferencedAssembly
		{
			[__DynamicallyInvokable]
			get
			{
				return this._containingAssembly;
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x0600467D RID: 18045 RVA: 0x00102578 File Offset: 0x00100778
		[__DynamicallyInvokable]
		public virtual string FileName
		{
			[__DynamicallyInvokable]
			get
			{
				return this._containingFileName;
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x0600467E RID: 18046 RVA: 0x00102580 File Offset: 0x00100780
		[__DynamicallyInvokable]
		public virtual ResourceLocation ResourceLocation
		{
			[__DynamicallyInvokable]
			get
			{
				return this._resourceLocation;
			}
		}

		// Token: 0x04001CE0 RID: 7392
		private Assembly _containingAssembly;

		// Token: 0x04001CE1 RID: 7393
		private string _containingFileName;

		// Token: 0x04001CE2 RID: 7394
		private ResourceLocation _resourceLocation;
	}
}
