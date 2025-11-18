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
	public class PROTOCOL_BATTLE_TIMEOUTCLIENT_REQ : GameClientPacket
	{
		private int int_0;

		public PROTOCOL_BATTLE_TIMEOUTCLIENT_REQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadD();
		}

		public override void Run()
		{
			SlotModel slotModel;
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null && room.GetSlot(this.int_0, out slotModel) && player.SlotId == slotModel.Id)
					{
						player.SendPacket(new PROTOCOL_BATTLE_TIMEOUTCLIENT_ACK());
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BATTLE_TIMEOUTCLIENT_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}