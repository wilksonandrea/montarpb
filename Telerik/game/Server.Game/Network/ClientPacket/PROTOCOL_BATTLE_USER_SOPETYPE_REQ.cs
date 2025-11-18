using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BATTLE_USER_SOPETYPE_REQ : GameClientPacket
	{
		private int int_0;

		public PROTOCOL_BATTLE_USER_SOPETYPE_REQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadC();
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
						player.Sight = this.int_0;
						using (PROTOCOL_BATTLE_USER_SOPETYPE_ACK pROTOCOLBATTLEUSERSOPETYPEACK = new PROTOCOL_BATTLE_USER_SOPETYPE_ACK(player))
						{
							room.SendPacketToPlayers(new PROTOCOL_BATTLE_USER_SOPETYPE_ACK(player));
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BATTLE_USER_SOPETYPE_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}