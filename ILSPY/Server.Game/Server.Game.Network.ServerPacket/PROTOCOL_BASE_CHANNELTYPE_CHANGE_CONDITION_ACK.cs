using System.Collections.Generic;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(2490);
		WriteB(method_0(SChannelXML.Servers));
	}

	private byte[] method_0(List<SChannelModel> list_0)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		syncServerPacket.WriteC((byte)list_0.Count);
		foreach (SChannelModel item in list_0)
		{
			syncServerPacket.WriteD(item.State ? 1 : 0);
			syncServerPacket.WriteB(ComDiv.AddressBytes(item.Host));
			syncServerPacket.WriteH(item.Port);
			syncServerPacket.WriteC((byte)item.Type);
			syncServerPacket.WriteH((ushort)item.MaxPlayers);
			syncServerPacket.WriteD(item.LastPlayers);
		}
		syncServerPacket.WriteC(0);
		return syncServerPacket.ToArray();
	}
}
