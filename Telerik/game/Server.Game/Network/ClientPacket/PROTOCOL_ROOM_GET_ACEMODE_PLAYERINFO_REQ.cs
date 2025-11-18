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
	public class PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_REQ : GameClientPacket
	{
		private int int_0;

		public PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_REQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadD();
		}

		public override void Run()
		{
			Account account;
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null && room.GetPlayerBySlot(this.int_0, out account))
					{
						this.Client.SendPacket(new PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK(account));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}