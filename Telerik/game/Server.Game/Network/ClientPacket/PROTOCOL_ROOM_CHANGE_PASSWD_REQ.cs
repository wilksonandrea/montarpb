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
	public class PROTOCOL_ROOM_CHANGE_PASSWD_REQ : GameClientPacket
	{
		private string string_0;

		public PROTOCOL_ROOM_CHANGE_PASSWD_REQ()
		{
		}

		public override void Read()
		{
			this.string_0 = base.ReadS(4);
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null && room.LeaderSlot == player.SlotId && room.Password != this.string_0)
					{
						room.Password = this.string_0;
						using (PROTOCOL_ROOM_CHANGE_PASSWD_ACK pROTOCOLROOMCHANGEPASSWDACK = new PROTOCOL_ROOM_CHANGE_PASSWD_ACK(this.string_0))
						{
							room.SendPacketToPlayers(pROTOCOLROOMCHANGEPASSWDACK);
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}
	}
}