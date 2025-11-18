namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Enums;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BATTLE_START_COUNTDOWN_ACK : GameServerPacket
    {
        private readonly CountDownEnum countDownEnum_0;

        public PROTOCOL_BATTLE_START_COUNTDOWN_ACK(CountDownEnum countDownEnum_1)
        {
            this.countDownEnum_0 = countDownEnum_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x1406);
            base.WriteC((byte) this.countDownEnum_0);
        }
    }
}

