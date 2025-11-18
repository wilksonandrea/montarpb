using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_REQ : GameClientPacket
	{
		public PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_REQ()
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
					if (room != null && room.State >= RoomState.LOADING && room.Slots[player.SlotId].State == SlotState.NORMAL)
					{
						this.Client.SendPacket(new PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_ACK(room));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}