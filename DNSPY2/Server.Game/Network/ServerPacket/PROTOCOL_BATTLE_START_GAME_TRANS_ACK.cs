using System;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200007E RID: 126
	public class PROTOCOL_BATTLE_START_GAME_TRANS_ACK : GameServerPacket
	{
		// Token: 0x06000154 RID: 340 RVA: 0x0000E3AC File Offset: 0x0000C5AC
		public PROTOCOL_BATTLE_START_GAME_TRANS_ACK(RoomModel roomModel_1, SlotModel slotModel_1, PlayerTitles playerTitles_1)
		{
			this.roomModel_0 = roomModel_1;
			this.slotModel_0 = slotModel_1;
			this.playerTitles_0 = playerTitles_1;
			if (roomModel_1 != null && slotModel_1 != null && playerTitles_1 != null)
			{
				this.playerEquipment_0 = slotModel_1.Equipment;
				if (this.playerEquipment_0 != null)
				{
					TeamEnum teamEnum = roomModel_1.ValidateTeam(slotModel_1.Team, slotModel_1.CostumeTeam);
					if (teamEnum == TeamEnum.FR_TEAM)
					{
						this.int_0 = this.playerEquipment_0.CharaRedId;
						return;
					}
					if (teamEnum == TeamEnum.CT_TEAM)
					{
						this.int_0 = this.playerEquipment_0.CharaBlueId;
					}
				}
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000E430 File Offset: 0x0000C630
		public override void Write()
		{
			base.WriteH(5128);
			base.WriteH(0);
			base.WriteD((uint)this.slotModel_0.PlayerId);
			base.WriteC(2);
			base.WriteC((byte)this.slotModel_0.Id);
			base.WriteD(this.int_0);
			base.WriteD(this.playerEquipment_0.WeaponPrimary);
			base.WriteD(this.playerEquipment_0.WeaponSecondary);
			base.WriteD(this.playerEquipment_0.WeaponMelee);
			base.WriteD(this.playerEquipment_0.WeaponExplosive);
			base.WriteD(this.playerEquipment_0.WeaponSpecial);
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
			base.WriteB(Bitwise.HexStringToByteArray("64 64 64 64 64"));
			base.WriteC((byte)this.playerTitles_0.Equiped1);
			base.WriteC((byte)this.playerTitles_0.Equiped2);
			base.WriteC((byte)this.playerTitles_0.Equiped3);
			base.WriteD(this.playerEquipment_0.AccessoryId);
			base.WriteD(this.playerEquipment_0.SprayId);
			base.WriteD(this.playerEquipment_0.NameCardId);
		}

		// Token: 0x040000F7 RID: 247
		private readonly RoomModel roomModel_0;

		// Token: 0x040000F8 RID: 248
		private readonly SlotModel slotModel_0;

		// Token: 0x040000F9 RID: 249
		private readonly PlayerEquipment playerEquipment_0;

		// Token: 0x040000FA RID: 250
		private readonly PlayerTitles playerTitles_0;

		// Token: 0x040000FB RID: 251
		private readonly int int_0;
	}
}
