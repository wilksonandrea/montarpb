using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001CB RID: 459
	public class PROTOCOL_ROOM_GET_USER_EQUIPMENT_REQ : GameClientPacket
	{
		// Token: 0x060004DD RID: 1245 RVA: 0x000057A5 File Offset: 0x000039A5
		public override void Read()
		{
			this.int_0 = (int)base.ReadC();
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00025C24 File Offset: 0x00023E24
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					SlotModel slotModel;
					if (room != null && player.SlotId != this.int_0 && room.GetSlot(this.int_0, out slotModel))
					{
						uint num = 0U;
						int[] array = new int[2];
						PlayerEquipment equipment = slotModel.Equipment;
						if (equipment != null)
						{
							TeamEnum teamEnum = room.ValidateTeam(slotModel.Team, slotModel.CostumeTeam);
							if (teamEnum == TeamEnum.FR_TEAM)
							{
								array[0] = equipment.CharaRedId;
							}
							else if (teamEnum == TeamEnum.CT_TEAM)
							{
								array[0] = equipment.CharaBlueId;
							}
							array[1] = equipment.AccessoryId;
						}
						else
						{
							num = 134217728U;
						}
						this.Client.SendPacket(new PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK(num, equipment, array, (byte)slotModel.Id));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_ROOM_GET_USER_EQUIPMENT_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_ROOM_GET_USER_EQUIPMENT_REQ()
		{
		}

		// Token: 0x04000377 RID: 887
		private int int_0;
	}
}
