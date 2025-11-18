using System;
using System.Collections.Generic;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000078 RID: 120
	public class PROTOCOL_BATTLE_RESPAWN_ACK : GameServerPacket
	{
		// Token: 0x06000142 RID: 322 RVA: 0x0000DA10 File Offset: 0x0000BC10
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

		// Token: 0x06000143 RID: 323 RVA: 0x0000DA9C File Offset: 0x0000BC9C
		public override void Write()
		{
			base.WriteH(5138);
			base.WriteD(this.slotModel_0.Id);
			RoomModel roomModel = this.roomModel_0;
			int num = roomModel.SpawnsCount;
			roomModel.SpawnsCount = num + 1;
			base.WriteD(num);
			SlotModel slotModel = this.slotModel_0;
			num = slotModel.SpawnsCount + 1;
			slotModel.SpawnsCount = num;
			base.WriteD(num);
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

		// Token: 0x06000144 RID: 324 RVA: 0x0000DC30 File Offset: 0x0000BE30
		private byte[] method_0(RoomModel roomModel_1, List<int> list_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				if (roomModel_1.IsDinoMode(""))
				{
					int num = ((list_1.Count == 1 || roomModel_1.IsDinoMode("CC")) ? 255 : roomModel_1.TRex);
					syncServerPacket.WriteC((byte)num);
					syncServerPacket.WriteC(10);
					for (int i = 0; i < list_1.Count; i++)
					{
						int num2 = list_1[i];
						if ((num2 != roomModel_1.TRex && roomModel_1.IsDinoMode("DE")) || roomModel_1.IsDinoMode("CC"))
						{
							syncServerPacket.WriteC((byte)num2);
						}
					}
					int num3 = 8 - list_1.Count - ((num == 255) ? 1 : 0);
					for (int j = 0; j < num3; j++)
					{
						syncServerPacket.WriteC(byte.MaxValue);
					}
					syncServerPacket.WriteC(byte.MaxValue);
				}
				else
				{
					syncServerPacket.WriteB(new byte[10]);
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x040000EA RID: 234
		private readonly RoomModel roomModel_0;

		// Token: 0x040000EB RID: 235
		private readonly SlotModel slotModel_0;

		// Token: 0x040000EC RID: 236
		private readonly PlayerEquipment playerEquipment_0;

		// Token: 0x040000ED RID: 237
		private readonly List<int> list_0;

		// Token: 0x040000EE RID: 238
		private readonly int int_0;
	}
}
