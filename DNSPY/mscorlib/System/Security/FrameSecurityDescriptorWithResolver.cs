using System;
using System.Reflection.Emit;

namespace System.Security
{
	// Token: 0x020001D7 RID: 471
	internal class FrameSecurityDescriptorWithResolver : FrameSecurityDescriptor
	{
		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06001C9D RID: 7325 RVA: 0x00061E54 File Offset: 0x00060054
		public DynamicResolver Resolver
		{
			get
			{
				return this.m_resolver;
			}
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x00061E5C File Offset: 0x0006005C
		public FrameSecurityDescriptorWithResolver()
		{
		}

		// Token: 0x040009FC RID: 2556
		private DynamicResolver m_resolver;
	}
}
