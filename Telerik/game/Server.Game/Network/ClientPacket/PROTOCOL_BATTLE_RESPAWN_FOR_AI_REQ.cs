using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BATTLE_RESPAWN_FOR_AI_REQ : GameClientPacket
	{
		private int int_0;

		public PROTOCOL_BATTLE_RESPAWN_FOR_AI_REQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadD();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null && room.State == RoomState.BATTLE && player.SlotId == room.LeaderSlot)
					{
						SlotModel slot = room.GetSlot(this.int_0);
						if (slot != null)
						{
							slot.AiLevel = room.IngameAiLevel;
							room.SpawnsCount++;
						}
						using (PROTOCOL_BATTLE_RESPAWN_FOR_AI_ACK pROTOCOLBATTLERESPAWNFORAIACK = new PROTOCOL_BATTLE_RESPAWN_FOR_AI_ACK(this.int_0))
						{
							room.SendPacketToPlayers(pROTOCOLBATTLERESPAWNFORAIACK, SlotState.BATTLE, 0);
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BATTLE_RESPAWN_FOR_AI_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}