using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BATTLE_RESPAWN_ACK : GameServerPacket
	{
		private readonly RoomModel roomModel_0;

		private readonly SlotModel slotModel_0;

		private readonly PlayerEquipment playerEquipment_0;

		private readonly List<int> list_0;

		private readonly int int_0;

		public PROTOCOL_BATTLE_RESPAWN_ACK(RoomModel roomModel_1, SlotModel slotModel_1)
		{
			this.roomModel_0 = roomModel_1;
			this.slotModel_0 = slotModel_1;
			if (roomModel_1 != null && slotModel_1 != null)
			{
				this.playerEquipment_0 = slotModel_1.Equipment;
				if (this.playerEquipment_0 != null)
				{
					TeamEnum teamEnum = roomModel_1.ValidateTeam(slotModel_1.Team, slotModel_1.CostumeTeam);
					if (teamEnum == TeamEnum.FR_TEAM)
					{
						this.int_0 = this.playerEquipment_0.CharaRedId;
					}
					else if (teamEnum == TeamEnum.CT_TEAM)
					{
						this.int_0 = this.playerEquipment_0.CharaBlueId;
					}
				}
				this.list_0 = AllUtils.GetDinossaurs(roomModel_1, false, slotModel_1.Id);
			}
		}

		private byte[] method_0(RoomModel roomModel_1, List<int> list_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				if (!roomModel_1.IsDinoMode(""))
				{
					syncServerPacket.WriteB(new byte[10]);
				}
				else
				{
					int ınt32 = (list_1.Count == 1 || roomModel_1.IsDinoMode("CC") ? 255 : roomModel_1.TRex);
					syncServerPacket.WriteC((byte)ınt32);
					syncServerPacket.WriteC(10);
					for (int i = 0; i < list_1.Count; i++)
					{
						int ıtem = list_1[i];
						if (ıtem != roomModel_1.TRex && roomModel_1.IsDinoMode("DE") || roomModel_1.IsDinoMode("CC"))
						{
							syncServerPacket.WriteC((byte)ıtem);
						}
					}
					int count = 8 - list_1.Count - (ınt32 == 255);
					for (int j = 0; j < count; j++)
					{
						syncServerPacket.WriteC(255);
					}
					syncServerPacket.WriteC(255);
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		public override void Write()
		{
			base.WriteH(5138);
			base.WriteD(this.slotModel_0.Id);
			RoomModel roomModel0 = this.roomModel_0;
			int spawnsCount = roomModel0.SpawnsCount;
			roomModel0.SpawnsCount = spawnsCount + 1;
			base.WriteD(spawnsCount);
			SlotModel slotModel0 = this.slotModel_0;
			spawnsCount = slotModel0.SpawnsCount + 1;
			slotModel0.SpawnsCount = spawnsCount;
			base.WriteD(spawnsCount);
			base.WriteD(this.playerEquipment_0.WeaponPrimary);
			base.WriteD(this.playerEquipment_0.WeaponSecondary);
			base.WriteD(this.playerEquipment_0.WeaponMelee);
			base.WriteD(this.playerEquipment_0.WeaponExplosive);
			base.WriteD(this.playerEquipment_0.WeaponSpecial);
			base.WriteB(Bitwise.HexStringToByteArray("64 64 64 64 64"));
			base.WriteD(this.int_0);
			base.WriteD(this.playerEquipment_0.PartHead);
			base.WriteD(this.playerEquipment_0.PartFace);
			base.WriteD(this.playerEquipment_0.PartJacket);
			base.WriteD(this.playerEquipment_0.PartPocket);
			base.WriteD(this.playerEquipment_0.PartGlove);
			base.WriteD(this.playerEquipment_0.PartBelt);
			base.WriteD(this.playerEquipment_0.PartHolster);
			base.WriteD(this.playerEquipment_0.PartSkin);
			base.WriteD(this.playerEquipment_0.BeretItem);
			base.WriteD(this.playerEquipment_0.AccessoryId);
			base.WriteB(this.method_0(this.roomModel_0, this.list_0));
		}
	}
}