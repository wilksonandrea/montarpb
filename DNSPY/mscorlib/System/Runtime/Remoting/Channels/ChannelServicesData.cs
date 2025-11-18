using System;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200082D RID: 2093
	internal class ChannelServicesData
	{
		// Token: 0x060059A1 RID: 22945 RVA: 0x0013BE6C File Offset: 0x0013A06C
		public ChannelServicesData()
		{
		}

		// Token: 0x040028D6 RID: 10454
		internal long remoteCalls;

		// Token: 0x040028D7 RID: 10455
		internal CrossContextChannel xctxmessageSink;

		// Token: 0x040028D8 RID: 10456
		internal CrossAppDomainChannel xadmessageSink;

		// Token: 0x040028D9 RID: 10457
		internal bool fRegisterWellKnownChannels;
	}
}
