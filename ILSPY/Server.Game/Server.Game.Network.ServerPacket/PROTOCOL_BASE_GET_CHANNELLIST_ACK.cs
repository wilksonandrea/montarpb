using System.Collections.Generic;
using Plugin.Core.Models;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_GET_CHANNELLIST_ACK : GameServerPacket
{
	private readonly SChannelModel schannelModel_0;

	private readonly List<ChannelModel> list_0;

	public PROTOCOL_BASE_GET_CHANNELLIST_ACK(SChannelModel schannelModel_1, List<ChannelModel> list_1)
	{
		schannelModel_0 = schannelModel_1;
		list_0 = list_1;
	}

	public override void Write()
	{
		WriteH(2333);
		WriteD(0);
		WriteD(1);
		WriteD(0);
		WriteH(0);
		WriteC(0);
		WriteC((byte)list_0.Count);
		foreach (ChannelModel item in list_0)
		{
			WriteH((ushort)item.Players.Count);
		}
		WriteH(310);
		WriteH((ushort)schannelModel_0.ChannelPlayers);
		WriteC((byte)list_0.Count);
		WriteC(0);
	}
}
