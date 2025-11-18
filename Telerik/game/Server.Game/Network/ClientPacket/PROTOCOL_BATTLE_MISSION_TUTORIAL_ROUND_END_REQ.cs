using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_REQ : GameClientPacket
	{
		public PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_REQ()
		{
		}

		public override void Read()
		{
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null)
					{
						room.State = RoomState.BATTLE_END;
						this.Client.SendPacket(new PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_ACK(room));
						this.Client.SendPacket(new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(room, 0, RoundEndType.Tutorial));
						this.Client.SendPacket(new PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK(room));
						if (room.State == RoomState.BATTLE_END)
						{
							room.State = RoomState.READY;
							this.Client.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(player));
							this.Client.SendPacket(new PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK(room));
						}
						AllUtils.ResetBattleInfo(room);
						this.Client.SendPacket(new PROTOCOL_ROOM_GET_SLOTINFO_ACK(room));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}