using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_REQ : GameClientPacket
{
	public override void Read()
	{
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null)
			{
				return;
			}
			RoomModel room = player.Room;
			if (room != null)
			{
				room.State = RoomState.BATTLE_END;
				Client.SendPacket(new PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_ACK(room));
				Client.SendPacket(new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(room, 0, RoundEndType.Tutorial));
				Client.SendPacket(new PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK(room));
				if (room.State == RoomState.BATTLE_END)
				{
					room.State = RoomState.READY;
					Client.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(player));
					Client.SendPacket(new PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK(room));
				}
				AllUtils.ResetBattleInfo(room);
				Client.SendPacket(new PROTOCOL_ROOM_GET_SLOTINFO_ACK(room));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
