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
	public class PROTOCOL_ROOM_GET_PLAYERINFO_REQ : GameClientPacket
	{
		private int int_0;

		public PROTOCOL_ROOM_GET_PLAYERINFO_REQ()
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
					if (room != null && player.SlotId != this.int_0 && room.GetPlayerBySlot(this.int_0, out account))
					{
						uint uInt32 = 0;
						int[] charaRedId = new int[2];
						SlotModel slot = room.GetSlot(account.SlotId);
						if (slot == null)
						{
							uInt32 = 134217728;
						}
						else
						{
							TeamEnum teamEnum = room.ValidateTeam(slot.Team, slot.CostumeTeam);
							if (teamEnum == TeamEnum.FR_TEAM)
							{
								charaRedId[0] = slot.Equipment.CharaRedId;
							}
							else if (teamEnum == TeamEnum.CT_TEAM)
							{
								charaRedId[0] = slot.Equipment.CharaBlueId;
							}
							charaRedId[1] = slot.Equipment.AccessoryId;
						}
						this.Client.SendPacket(new PROTOCOL_ROOM_GET_PLAYERINFO_ACK(uInt32, account, charaRedId));
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