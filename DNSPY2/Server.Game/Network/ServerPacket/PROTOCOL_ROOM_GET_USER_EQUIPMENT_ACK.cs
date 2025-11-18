using System;
using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000F9 RID: 249
	public class PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK : GameServerPacket
	{
		// Token: 0x0600025F RID: 607 RVA: 0x000047E3 File Offset: 0x000029E3
		public PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK(uint uint_1, PlayerEquipment playerEquipment_1, int[] int_1, byte byte_1)
		{
			this.uint_0 = uint_1;
			this.playerEquipment_0 = playerEquipment_1;
			this.int_0 = int_1;
			this.byte_0 = byte_1;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00012F48 File Offset: 0x00011148
		public override void Write()
		{
			base.WriteH(3666);
			base.WriteH(0);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0U)
			{
				base.WriteD(this.int_0[1]);
				base.WriteC(10);
				base.WriteD(this.int_0[0]);
				base.WriteD(this.playerEquipment_0.PartHead);
				base.WriteD(this.playerEquipment_0.PartFace);
				base.WriteD(this.playerEquipment_0.PartJacket);
				base.WriteD(this.playerEquipment_0.PartPocket);
				base.WriteD(this.playerEquipment_0.PartGlove);
				base.WriteD(this.playerEquipment_0.PartBelt);
				base.WriteD(this.playerEquipment_0.PartHolster);
				base.WriteD(this.playerEquipment_0.PartSkin);
				base.WriteD(this.playerEquipment_0.BeretItem);
				base.WriteC(5);
				base.WriteD(this.playerEquipment_0.WeaponPrimary);
				base.WriteD(this.playerEquipment_0.WeaponSecondary);
				base.WriteD(this.playerEquipment_0.WeaponMelee);
				base.WriteD(this.playerEquipment_0.WeaponExplosive);
				base.WriteD(this.playerEquipment_0.WeaponSpecial);
				base.WriteC(2);
				base.WriteD(this.playerEquipment_0.CharaRedId);
				base.WriteD(this.playerEquipment_0.CharaBlueId);
				base.WriteC(this.byte_0);
			}
		}

		// Token: 0x040001D0 RID: 464
		private readonly uint uint_0;

		// Token: 0x040001D1 RID: 465
		private readonly PlayerEquipment playerEquipment_0;

		// Token: 0x040001D2 RID: 466
		private readonly int[] int_0;

		// Token: 0x040001D3 RID: 467
		private readonly byte byte_0;
	}
}
