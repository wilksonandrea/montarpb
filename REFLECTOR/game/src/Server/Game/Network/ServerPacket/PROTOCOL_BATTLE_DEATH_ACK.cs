namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

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
            base.WriteH((short) 0x1410);
            base.WriteC((byte) this.fragInfos_0.KillingType);
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
                FragModel model = this.fragInfos_0.Frags[i];
                base.WriteC(model.VictimSlot);
                base.WriteC(model.WeaponClass);
                base.WriteC(model.HitspotInfo);
                base.WriteH((ushort) model.KillFlag);
                base.WriteC(model.Unk);
                base.WriteT(model.X);
                base.WriteT(model.Y);
                base.WriteT(model.Z);
                base.WriteC(model.AssistSlot);
                base.WriteB(model.Unks);
            }
            base.WriteH((ushort) this.roomModel_0.FRKills);
            base.WriteH((ushort) this.roomModel_0.FRDeaths);
            base.WriteH((ushort) this.roomModel_0.FRAssists);
            base.WriteH((ushort) this.roomModel_0.CTKills);
            base.WriteH((ushort) this.roomModel_0.CTDeaths);
            base.WriteH((ushort) this.roomModel_0.CTAssists);
            foreach (SlotModel model2 in this.roomModel_0.Slots)
            {
                base.WriteH((ushort) model2.AllKills);
                base.WriteH((ushort) model2.AllDeaths);
                base.WriteH((ushort) model2.AllAssists);
            }
            this.WriteC((this.slotModel_0.KillsOnLife == 2) ? ((byte) 1) : ((this.slotModel_0.KillsOnLife == 3) ? ((byte) 2) : ((this.slotModel_0.KillsOnLife > 3) ? ((byte) 3) : ((byte) 0))));
            base.WriteH((ushort) this.fragInfos_0.Score);
            if (this.roomModel_0.IsDinoMode("DE"))
            {
                base.WriteH((ushort) this.roomModel_0.FRDino);
                base.WriteH((ushort) this.roomModel_0.CTDino);
            }
        }
    }
}

