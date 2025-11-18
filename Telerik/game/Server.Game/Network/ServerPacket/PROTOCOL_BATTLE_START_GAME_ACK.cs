using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BATTLE_START_GAME_ACK : GameServerPacket
	{
		private readonly RoomModel roomModel_0;

		public PROTOCOL_BATTLE_START_GAME_ACK(RoomModel roomModel_1)
		{
			this.roomModel_0 = roomModel_1;
		}

		private byte[] method_0(RoomModel roomModel_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC((byte)roomModel_1.GetReadyPlayers());
				SlotModel[] slots = roomModel_1.Slots;
				for (int i = 0; i < (int)slots.Length; i++)
				{
					SlotModel slotModel = slots[i];
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

		private byte[] method_1(RoomModel roomModel_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC((byte)((int)roomModel_1.Slots.Length));
				SlotModel[] slots = roomModel_1.Slots;
				for (int i = 0; i < (int)slots.Length; i++)
				{
					syncServerPacket.WriteC((byte)slots[i].Team);
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		private byte[] method_2(RoomModel roomModel_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC((byte)roomModel_1.GetReadyPlayers());
				SlotModel[] slots = roomModel_1.Slots;
				for (int i = 0; i < (int)slots.Length; i++)
				{
					SlotModel slotModel = slots[i];
					if (slotModel.State >= SlotState.READY && slotModel.Equipment != null)
					{
						Account playerBySlot = roomModel_1.GetPlayerBySlot(slotModel);
						if (playerBySlot != null && playerBySlot.SlotId == slotModel.Id)
						{
							syncServerPacket.WriteC((byte)slotModel.Id);
							PlayerEquipment equipment = playerBySlot.Equipment;
							PlayerTitles title = playerBySlot.Title;
							int charaRedId = 0;
							if (equipment != null && title != null)
							{
								TeamEnum teamEnum = roomModel_1.ValidateTeam(slotModel.Team, slotModel.CostumeTeam);
								if (teamEnum == TeamEnum.FR_TEAM)
								{
									charaRedId = equipment.CharaRedId;
								}
								else if (teamEnum == TeamEnum.CT_TEAM)
								{
									charaRedId = equipment.CharaBlueId;
								}
								syncServerPacket.WriteD(charaRedId);
								syncServerPacket.WriteD(equipment.WeaponPrimary);
								syncServerPacket.WriteD(equipment.WeaponSecondary);
								syncServerPacket.WriteD(equipment.WeaponMelee);
								syncServerPacket.WriteD(equipment.WeaponExplosive);
								syncServerPacket.WriteD(equipment.WeaponSpecial);
								syncServerPacket.WriteD(charaRedId);
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
	}
}