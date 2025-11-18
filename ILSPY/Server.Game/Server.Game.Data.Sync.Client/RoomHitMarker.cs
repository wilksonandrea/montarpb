using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Data.XML;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Sync.Client;

public static class RoomHitMarker
{
	public static void Load(SyncClientPacket C)
	{
		int id = C.ReadH();
		int ıd = C.ReadH();
		short serverId = C.ReadH();
		byte slotId = C.ReadC();
		byte b = C.ReadC();
		byte b2 = C.ReadC();
		int num = C.ReadD();
		if (C.ToArray().Length > 15)
		{
			CLogger.Print($"Invalid Hit (Length > 15): {C.ToArray().Length}", LoggerType.Warning);
		}
		ChannelModel channel = ChannelsXML.GetChannel(serverId, ıd);
		if (channel == null)
		{
			return;
		}
		RoomModel room = channel.GetRoom(id);
		if (room == null || room.State != RoomState.BATTLE)
		{
			return;
		}
		Account playerBySlot = room.GetPlayerBySlot(slotId);
		if (playerBySlot != null)
		{
			string string_ = "";
			if (b == 10)
			{
				string_ = Translation.GetLabel("LifeRestored", num);
			}
			switch (b2)
			{
			case 0:
				string_ = Translation.GetLabel("HitMarker1", num);
				break;
			case 1:
				string_ = Translation.GetLabel("HitMarker2", num);
				break;
			case 2:
				string_ = Translation.GetLabel("HitMarker3");
				break;
			case 3:
				string_ = Translation.GetLabel("HitMarker4");
				break;
			}
			playerBySlot.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK(Translation.GetLabel("HitMarkerName"), playerBySlot.GetSessionId(), 0, bool_1: true, string_));
		}
	}
}
