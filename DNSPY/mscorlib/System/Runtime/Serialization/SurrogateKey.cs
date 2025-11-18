using System;

namespace System.Runtime.Serialization
{
	// Token: 0x0200075C RID: 1884
	[Serializable]
	internal class SurrogateKey
	{
		// Token: 0x060052F7 RID: 21239 RVA: 0x00123875 File Offset: 0x00121A75
		internal SurrogateKey(Type type, StreamingContext context)
		{
			this.m_type = type;
			this.m_context = context;
		}

		// Token: 0x060052F8 RID: 21240 RVA: 0x0012388B File Offset: 0x00121A8B
		public override int GetHashCode()
		{
			return this.m_type.GetHashCode();
		}

		// Token: 0x040024CF RID: 9423
		internal Type m_type;

		// Token: 0x040024D0 RID: 9424
		internal StreamingContext m_context;
	}
}
