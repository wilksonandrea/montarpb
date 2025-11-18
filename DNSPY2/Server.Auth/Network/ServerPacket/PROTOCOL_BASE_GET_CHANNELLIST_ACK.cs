using System;
using System.Collections.Generic;
using Plugin.Core.Models;
using Server.Auth.Data.Models;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x0200001C RID: 28
	public class PROTOCOL_BASE_GET_CHANNELLIST_ACK : AuthServerPacket
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00002732 File Offset: 0x00000932
		public PROTOCOL_BASE_GET_CHANNELLIST_ACK(SChannelModel schannelModel_1, List<ChannelModel> list_1)
		{
			this.schannelModel_0 = schannelModel_1;
			this.list_0 = list_1;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004AE8 File Offset: 0x00002CE8
		public override void Write()
		{
			base.WriteH(2333);
			base.WriteD(0);
			base.WriteD(1);
			base.WriteD(0);
			base.WriteH(0);
			base.WriteC(0);
			base.WriteC((byte)this.list_0.Count);
			foreach (ChannelModel channelModel in this.list_0)
			{
				base.WriteH((ushort)channelModel.TotalPlayers);
			}
			base.WriteH(310);
			base.WriteH((ushort)this.schannelModel_0.ChannelPlayers);
			base.WriteC((byte)this.list_0.Count);
			base.WriteC(0);
		}

		// Token: 0x04000037 RID: 55
		private readonly SChannelModel schannelModel_0;

		// Token: 0x04000038 RID: 56
		private readonly List<ChannelModel> list_0;
	}
}
