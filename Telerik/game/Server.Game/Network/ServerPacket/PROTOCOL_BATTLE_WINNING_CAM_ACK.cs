using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Network;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BATTLE_WINNING_CAM_ACK : GameServerPacket
	{
		private readonly FragInfos fragInfos_0;

		public PROTOCOL_BATTLE_WINNING_CAM_ACK(FragInfos fragInfos_1)
		{
			this.fragInfos_0 = fragInfos_1;
		}

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
			for (int i = 0; i < this.fragInfos_0.KillsCount; i++)
			{
				FragModel ıtem = this.fragInfos_0.Frags[i];
				base.WriteC(ıtem.VictimSlot);
				base.WriteC(ıtem.WeaponClass);
				base.WriteC(ıtem.HitspotInfo);
				base.WriteH((ushort)ıtem.KillFlag);
				base.WriteC(ıtem.Unk);
				base.WriteT(ıtem.X);
				base.WriteT(ıtem.Y);
				base.WriteT(ıtem.Z);
				base.WriteC(ıtem.AssistSlot);
				base.WriteB(ıtem.Unks);
			}
		}
	}
}