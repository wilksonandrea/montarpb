namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Enums;
    using Plugin.Core.Utility;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK : GameServerPacket
    {
        private readonly Account account_0;
        private readonly EventErrorEnum eventErrorEnum_0;

        public PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK(EventErrorEnum eventErrorEnum_1, Account account_1)
        {
            this.eventErrorEnum_0 = eventErrorEnum_1;
            this.account_0 = account_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x93d);
            base.WriteD((uint) this.eventErrorEnum_0);
            if (this.eventErrorEnum_0 == EventErrorEnum.SUCCESS)
            {
                base.WriteD(this.account_0.Gold);
                base.WriteC((byte) this.account_0.Mission.ActualMission);
                base.WriteC((byte) this.account_0.Mission.ActualMission);
                base.WriteC((byte) this.account_0.Mission.Card1);
                base.WriteC((byte) this.account_0.Mission.Card2);
                base.WriteC((byte) this.account_0.Mission.Card3);
                base.WriteC((byte) this.account_0.Mission.Card4);
                base.WriteB(ComDiv.GetMissionCardFlags(this.account_0.Mission.Mission1, this.account_0.Mission.List1));
                base.WriteB(ComDiv.GetMissionCardFlags(this.account_0.Mission.Mission2, this.account_0.Mission.List2));
                base.WriteB(ComDiv.GetMissionCardFlags(this.account_0.Mission.Mission3, this.account_0.Mission.List3));
                base.WriteB(ComDiv.GetMissionCardFlags(this.account_0.Mission.Mission4, this.account_0.Mission.List4));
                base.WriteC((byte) this.account_0.Mission.Mission1);
                base.WriteC((byte) this.account_0.Mission.Mission2);
                base.WriteC((byte) this.account_0.Mission.Mission3);
                base.WriteC((byte) this.account_0.Mission.Mission4);
                base.WriteD(this.account_0.Cash);
            }
        }
    }
}

