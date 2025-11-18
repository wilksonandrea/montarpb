using System.Collections.Generic;
using Plugin.Core.Models;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK : GameServerPacket
{
	private readonly uint uint_0;

	private readonly List<QuickstartModel> list_0;

	private readonly QuickstartModel quickstartModel_0;

	private readonly RoomModel roomModel_0;

	public PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK(uint uint_1, List<QuickstartModel> list_1, RoomModel roomModel_1, QuickstartModel quickstartModel_1)
	{
		uint_0 = uint_1;
		list_0 = list_1;
		quickstartModel_0 = quickstartModel_1;
		roomModel_0 = roomModel_1;
	}

	public override void Write()
	{
		WriteH(5378);
		WriteD(uint_0);
		foreach (QuickstartModel item in list_0)
		{
			WriteC((byte)item.MapId);
			WriteC((byte)item.Rule);
			WriteC((byte)item.StageOptions);
			WriteC((byte)item.Type);
		}
		if (uint_0 == 0)
		{
			WriteC((byte)roomModel_0.ChannelId);
			WriteD(roomModel_0.RoomId);
			WriteH(1);
			if (uint_0 != 0)
			{
				WriteC((byte)quickstartModel_0.MapId);
				WriteC((byte)quickstartModel_0.Rule);
				WriteC((byte)quickstartModel_0.StageOptions);
				WriteC((byte)quickstartModel_0.Type);
			}
			else
			{
				WriteC(0);
				WriteC(0);
				WriteC(0);
				WriteC(0);
			}
			WriteD(0);
			WriteD(0);
			WriteD(0);
			WriteD(0);
		}
	}
}
