using System;
using Plugin.Core.Models;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000061 RID: 97
	public class PROTOCOL_BATTLE_DEATH_ACK : GameServerPacket
	{
		// Token: 0x06000103 RID: 259 RVA: 0x000032A5 File Offset: 0x000014A5
		public PROTOCOL_BATTLE_DEATH_ACK(RoomModel roomModel_1, FragInfos fragInfos_1, SlotModel slotModel_1)
		{
			this.roomModel_0 = roomModel_1;
			this.fragInfos_0 = fragInfos_1;
			this.slotModel_0 = slotModel_1;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000C0E8 File Offset: 0x0000A2E8
		public override void Write()
		{
			base.WriteH(5136);
			base.WriteC((byte)this.fragInfos_0.KillingType);
			base.WriteC(this.fragInfos_0.KillsCount);
			base.WriteC(this.fragInfos_0.KillerSlot);
			base.WriteD(this.fragInfos_0.WeaponId);
			base.WriteT(this.fragInfos_0.X);
			base.WriteT(this.fragInfos_0.Y);
			base.WriteT(this.fragInfos_0.Z);
			base.WriteC(this.fragInfos_0.Flag);
			base.WriteC(this.fragInfos_0.Unk);
			for (int i = 0; i < (int)this.fragInfos_0.KillsCount; i++)
			{
				FragModel fragModel = this.fragInfos_0.Frags[i];
				base.WriteC(fragModel.VictimSlot);
				base.WriteC(fragModel.WeaponClass);
				base.WriteC(fragModel.HitspotInfo);
				base.WriteH((ushort)fragModel.KillFlag);
				base.WriteC(fragModel.Unk);
				base.WriteT(fragModel.X);
				base.WriteT(fragModel.Y);
				base.WriteT(fragModel.Z);
				base.WriteC(fragModel.AssistSlot);
				base.WriteB(fragModel.Unks);
			}
			base.WriteH((ushort)this.roomModel_0.FRKills);
			base.WriteH((ushort)this.roomModel_0.FRDeaths);
			base.WriteH((ushort)this.roomModel_0.FRAssists);
			base.WriteH((ushort)this.roomModel_0.CTKills);
			base.WriteH((ushort)this.roomModel_0.CTDeaths);
			base.WriteH((ushort)this.roomModel_0.CTAssists);
			foreach (SlotModel slotModel in this.roomModel_0.Slots)
			{
				base.WriteH((ushort)slotModel.AllKills);
				base.WriteH((ushort)slotModel.AllDeaths);
				base.WriteH((ushort)slotModel.AllAssists);
			}
			base.WriteC((this.slotModel_0.KillsOnLife == 2) ? 1 : ((this.slotModel_0.KillsOnLife == 3) ? 2 : ((this.slotModel_0.KillsOnLife > 3) ? 3 : 0)));
			base.WriteH((ushort)this.fragInfos_0.Score);
			if (this.roomModel_0.IsDinoMode("DE"))
			{
				base.WriteH((ushort)this.roomModel_0.FRDino);
				base.WriteH((ushort)this.roomModel_0.CTDino);
			}
		}

		// Token: 0x040000BC RID: 188
		private readonly RoomModel roomModel_0;

		// Token: 0x040000BD RID: 189
		private readonly FragInfos fragInfos_0;

		// Token: 0x040000BE RID: 190
		private readonly SlotModel slotModel_0;
	}
}
