using System;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200007D RID: 125
	public class PROTOCOL_BATTLE_START_GAME_ACK : GameServerPacket
	{
		// Token: 0x0600014F RID: 335 RVA: 0x000035D0 File Offset: 0x000017D0
		public PROTOCOL_BATTLE_START_GAME_ACK(RoomModel roomModel_1)
		{
			this.roomModel_0 = roomModel_1;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000DFD0 File Offset: 0x0000C1D0
		public override void Write()
		{
			base.WriteH(5127);
			base.WriteH(0);
			base.WriteB(this.method_0(this.roomModel_0));
			base.WriteB(this.method_1(this.roomModel_0));
			base.WriteB(this.method_2(this.roomModel_0));
			base.WriteC((byte)this.roomModel_0.MapId);
			base.WriteC((byte)this.roomModel_0.Rule);
			base.WriteC((byte)this.roomModel_0.Stage);
			base.WriteC((byte)this.roomModel_0.RoomType);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000E06C File Offset: 0x0000C26C
		private byte[] method_0(RoomModel roomModel_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC((byte)roomModel_1.GetReadyPlayers());
				foreach (SlotModel slotModel in roomModel_1.Slots)
				{
					if (slotModel.State >= SlotState.READY && slotModel.Equipment != null)
					{
						Account playerBySlot = roomModel_1.GetPlayerBySlot(slotModel);
						if (playerBySlot != null && playerBySlot.SlotId == slotModel.Id)
						{
							syncServerPacket.WriteD((uint)playerBySlot.PlayerId);
						}
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000E108 File Offset: 0x0000C308
		private byte[] method_1(RoomModel roomModel_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC((byte)roomModel_1.Slots.Length);
				foreach (SlotModel slotModel in roomModel_1.Slots)
				{
					syncServerPacket.WriteC((byte)slotModel.Team);
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000E178 File Offset: 0x0000C378
		private byte[] method_2(RoomModel roomModel_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC((byte)roomModel_1.GetReadyPlayers());
				foreach (SlotModel slotModel in roomModel_1.Slots)
				{
					if (slotModel.State >= SlotState.READY && slotModel.Equipment != null)
					{
						Account playerBySlot = roomModel_1.GetPlayerBySlot(slotModel);
						if (playerBySlot != null && playerBySlot.SlotId == slotModel.Id)
						{
							syncServerPacket.WriteC((byte)slotModel.Id);
							PlayerEquipment equipment = playerBySlot.Equipment;
							PlayerTitles title = playerBySlot.Title;
							int num = 0;
							if (equipment != null && title != null)
							{
								TeamEnum teamEnum = roomModel_1.ValidateTeam(slotModel.Team, slotModel.CostumeTeam);
								if (teamEnum == TeamEnum.FR_TEAM)
								{
									num = equipment.CharaRedId;
								}
								else if (teamEnum == TeamEnum.CT_TEAM)
								{
									num = equipment.CharaBlueId;
								}
								syncServerPacket.WriteD(num);
								syncServerPacket.WriteD(equipment.WeaponPrimary);
								syncServerPacket.WriteD(equipment.WeaponSecondary);
								syncServerPacket.WriteD(equipment.WeaponMelee);
								syncServerPacket.WriteD(equipment.WeaponExplosive);
								syncServerPacket.WriteD(equipment.WeaponSpecial);
								syncServerPacket.WriteD(num);
								syncServerPacket.WriteD(equipment.PartHead);
								syncServerPacket.WriteD(equipment.PartFace);
								syncServerPacket.WriteD(equipment.PartJacket);
								syncServerPacket.WriteD(equipment.PartPocket);
								syncServerPacket.WriteD(equipment.PartGlove);
								syncServerPacket.WriteD(equipment.PartBelt);
								syncServerPacket.WriteD(equipment.PartHolster);
								syncServerPacket.WriteD(equipment.PartSkin);
								syncServerPacket.WriteD(equipment.BeretItem);
								syncServerPacket.WriteB(Bitwise.HexStringToByteArray("64 64 64 64 64"));
								syncServerPacket.WriteC((byte)title.Equiped1);
								syncServerPacket.WriteC((byte)title.Equiped2);
								syncServerPacket.WriteC((byte)title.Equiped3);
								syncServerPacket.WriteD(equipment.AccessoryId);
								syncServerPacket.WriteD(equipment.SprayId);
								syncServerPacket.WriteD(equipment.NameCardId);
							}
						}
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x040000F6 RID: 246
		private readonly RoomModel roomModel_0;
	}
}
