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
	public class PROTOCOL_BASE_GET_ROOM_USER_DETAIL_INFO_REQ : GameClientPacket
	{
		private int int_0;

		public PROTOCOL_BASE_GET_ROOM_USER_DETAIL_INFO_REQ()
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
					if (room != null && room.GetSlot(this.int_0, out slotModel))
					{
						Account playerBySlot = room.GetPlayerBySlot(slotModel);
						if (playerBySlot != null)
						{
							if (player.Nickname != playerBySlot.Nickname)
							{
								player.FindPlayer = playerBySlot.Nickname;
							}
							int charaRedId = 2147483647;
							TeamEnum teamEnum = room.ValidateTeam(slotModel.Team, slotModel.CostumeTeam);
							if (teamEnum == TeamEnum.FR_TEAM)
							{
								charaRedId = playerBySlot.Equipment.CharaRedId;
							}
							else if (teamEnum == TeamEnum.CT_TEAM)
							{
								charaRedId = playerBySlot.Equipment.CharaBlueId;
							}
							this.Client.SendPacket(new PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK(0, playerBySlot, charaRedId));
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BASE_GET_ROOM_USER_DETAIL_INFO_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}