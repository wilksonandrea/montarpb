using System;
using System.Collections.Generic;
using Plugin.Core.Models;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000050 RID: 80
	public class PROTOCOL_BASE_GET_CHANNELLIST_ACK : GameServerPacket
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x00003023 File Offset: 0x00001223
		public PROTOCOL_BASE_GET_CHANNELLIST_ACK(SChannelModel schannelModel_1, List<ChannelModel> list_1)
		{
			this.schannelModel_0 = schannelModel_1;
			this.list_0 = list_1;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000B5E8 File Offset: 0x000097E8
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
				base.WriteH((ushort)channelModel.Players.Count);
			}
			base.WriteH(310);
			base.WriteH((ushort)this.schannelModel_0.ChannelPlayers);
			base.WriteC((byte)this.list_0.Count);
			base.WriteC(0);
		}

		// Token: 0x040000A5 RID: 165
		private readonly SChannelModel schannelModel_0;

		// Token: 0x040000A6 RID: 166
		private readonly List<ChannelModel> list_0;
	}
}
