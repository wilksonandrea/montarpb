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
	public class PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_REQ : GameClientPacket
	{
		public PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_REQ()
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
					if (room != null && room.State == RoomState.BATTLE && room.IngameAiLevel < 10)
					{
						SlotModel slot = room.GetSlot(player.SlotId);
						if (slot != null && slot.State == SlotState.BATTLE)
						{
							if (room.IngameAiLevel <= 9)
							{
								RoomModel ıngameAiLevel = room;
								ıngameAiLevel.IngameAiLevel = (byte)(ıngameAiLevel.IngameAiLevel + 1);
							}
							using (PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK pROTOCOLBATTLECHANGEDIFFICULTYLEVELACK = new PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK(room))
							{
								room.SendPacketToPlayers(pROTOCOLBATTLECHANGEDIFFICULTYLEVELACK, SlotState.READY, 1);
							}
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}