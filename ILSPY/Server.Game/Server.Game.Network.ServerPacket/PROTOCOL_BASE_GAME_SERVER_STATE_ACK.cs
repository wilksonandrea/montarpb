using System.Collections.Generic;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Data.XML;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_GAME_SERVER_STATE_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(2400);
		WriteB(method_0(SChannelXML.Servers));
		WriteC(0);
	}

	private byte[] method_0(List<SChannelModel> list_0)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		syncServerPacket.WriteC((byte)list_0.Count);
		foreach (SChannelModel item in list_0)
		{
			syncServerPacket.WriteD(item.State ? 1 : 0);
			syncServerPacket.WriteB(ComDiv.AddressBytes(item.Host));
			syncServerPacket.WriteB(ComDiv.AddressBytes(item.Host));
			syncServerPacket.WriteH(item.Port);
			syncServerPacket.WriteC((byte)item.Type);
			syncServerPacket.WriteH((ushort)item.MaxPlayers);
			syncServerPacket.WriteD(item.LastPlayers);
			if (item.Id == 0)
			{
				syncServerPacket.WriteB(Bitwise.HexStringToByteArray("01 01 01 01 01 01 01 01 01 01 0E 00 00 00 00"));
				continue;
			}
			foreach (ChannelModel channel in ChannelsXML.GetChannels(item.Id))
			{
				syncServerPacket.WriteC((byte)channel.Type);
			}
			syncServerPacket.WriteC((byte)item.Type);
			syncServerPacket.WriteC(0);
			syncServerPacket.WriteH(0);
		}
		return syncServerPacket.ToArray();
	}
}
