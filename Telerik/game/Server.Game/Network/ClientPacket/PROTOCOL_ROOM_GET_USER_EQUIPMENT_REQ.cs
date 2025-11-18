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
	public class PROTOCOL_ROOM_GET_USER_EQUIPMENT_REQ : GameClientPacket
	{
		private int int_0;

		public PROTOCOL_ROOM_GET_USER_EQUIPMENT_REQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadC();
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
					if (room != null && player.SlotId != this.int_0 && room.GetSlot(this.int_0, out slotModel))
					{
						uint uInt32 = 0;
						int[] charaRedId = new int[2];
						PlayerEquipment equipment = slotModel.Equipment;
						if (equipment == null)
						{
							uInt32 = 134217728;
						}
						else
						{
							TeamEnum teamEnum = room.ValidateTeam(slotModel.Team, slotModel.CostumeTeam);
							if (teamEnum == TeamEnum.FR_TEAM)
							{
								charaRedId[0] = equipment.CharaRedId;
							}
							else if (teamEnum == TeamEnum.CT_TEAM)
							{
								charaRedId[0] = equipment.CharaBlueId;
							}
							charaRedId[1] = equipment.AccessoryId;
						}
						this.Client.SendPacket(new PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK(uInt32, equipment, charaRedId, (byte)slotModel.Id));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_ROOM_GET_USER_EQUIPMENT_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}