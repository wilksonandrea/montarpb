using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BATTLE_DEATH_ACK : GameServerPacket
	{
		private readonly RoomModel roomModel_0;

		private readonly FragInfos fragInfos_0;

		private readonly SlotModel slotModel_0;

		public PROTOCOL_BATTLE_DEATH_ACK(RoomModel roomModel_1, FragInfos fragInfos_1, SlotModel slotModel_1)
		{
			this.roomModel_0 = roomModel_1;
			this.fragInfos_0 = fragInfos_1;
			this.slotModel_0 = slotModel_1;
		}

		public override void Write()
		{
			object obj;
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
			base.WriteH((ushort)this.roomModel_0.FRKills);
			base.WriteH((ushort)this.roomModel_0.FRDeaths);
			base.WriteH((ushort)this.roomModel_0.FRAssists);
			base.WriteH((ushort)this.roomModel_0.CTKills);
			base.WriteH((ushort)this.roomModel_0.CTDeaths);
			base.WriteH((ushort)this.roomModel_0.CTAssists);
			SlotModel[] slots = this.roomModel_0.Slots;
			for (int j = 0; j < (int)slots.Length; j++)
			{
				SlotModel slotModel = slots[j];
				base.WriteH((ushort)slotModel.AllKills);
				base.WriteH((ushort)slotModel.AllDeaths);
				base.WriteH((ushort)slotModel.AllAssists);
			}
			if (this.slotModel_0.KillsOnLife == 2)
			{
				obj = 1;
			}
			else if (this.slotModel_0.KillsOnLife == 3)
			{
				obj = 2;
			}
			else if (this.slotModel_0.KillsOnLife > 3)
			{
				obj = 3;
			}
			else
			{
				obj = null;
			}
			base.WriteC((byte)obj);
			base.WriteH((ushort)this.fragInfos_0.Score);
			if (this.roomModel_0.IsDinoMode("DE"))
			{
				base.WriteH((ushort)this.roomModel_0.FRDino);
				base.WriteH((ushort)this.roomModel_0.CTDino);
			}
		}
	}
}