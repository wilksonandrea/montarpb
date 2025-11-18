namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BATTLE_WINNING_CAM_ACK : GameServerPacket
    {
        private readonly FragInfos fragInfos_0;

        public PROTOCOL_BATTLE_WINNING_CAM_ACK(FragInfos fragInfos_1)
        {
            this.fragInfos_0 = fragInfos_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x149f);
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
        }
    }
}

