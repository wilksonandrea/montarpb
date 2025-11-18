using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BASE_GET_CHANNELLIST_ACK : GameServerPacket
	{
		private readonly SChannelModel schannelModel_0;

		private readonly List<ChannelModel> list_0;

		public PROTOCOL_BASE_GET_CHANNELLIST_ACK(SChannelModel schannelModel_1, List<ChannelModel> list_1)
		{
			this.schannelModel_0 = schannelModel_1;
			this.list_0 = list_1;
		}

		public override void Write()
		{
			base.WriteH(2333);
			base.WriteD(0);
			base.WriteD(1);
			base.WriteD(0);
			base.WriteH(0);
			base.WriteC(0);
			base.WriteC((byte)this.list_0.Count);
			foreach (ChannelModel list0 in this.list_0)
			{
				base.WriteH((ushort)list0.Players.Count);
			}
			base.WriteH(310);
			base.WriteH((ushort)this.schannelModel_0.ChannelPlayers);
			base.WriteC((byte)this.list_0.Count);
			base.WriteC(0);
		}
	}
}