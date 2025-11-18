using System;
using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000011 RID: 17
	public class PROTOCOL_BATTLE_WINNING_CAM_ACK : GameServerPacket
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00002750 File Offset: 0x00000950
		public PROTOCOL_BATTLE_WINNING_CAM_ACK(FragInfos fragInfos_1)
		{
			this.fragInfos_0 = fragInfos_1;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00009490 File Offset: 0x00007690
		public override void Write()
		{
			base.WriteH(5279);
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
		}

		// Token: 0x04000025 RID: 37
		private readonly FragInfos fragInfos_0;
	}
}
