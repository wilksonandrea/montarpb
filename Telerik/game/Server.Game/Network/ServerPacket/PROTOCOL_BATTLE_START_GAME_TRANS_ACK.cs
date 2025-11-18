using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BATTLE_START_GAME_TRANS_ACK : GameServerPacket
	{
		private readonly RoomModel roomModel_0;

		private readonly SlotModel slotModel_0;

		private readonly PlayerEquipment playerEquipment_0;

		private readonly PlayerTitles playerTitles_0;

		private readonly int int_0;

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
	}
}