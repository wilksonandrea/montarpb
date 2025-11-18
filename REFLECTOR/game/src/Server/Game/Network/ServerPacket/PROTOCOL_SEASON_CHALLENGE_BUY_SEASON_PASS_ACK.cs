namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Plugin.Core.XML;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK : GameServerPacket
    {
        private readonly uint uint_0;
        private readonly BattlePassModel battlePassModel_0;
        private readonly PlayerBattlepass playerBattlepass_0;
        private readonly (int, int, int, int) valueTuple_0;

        public PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK(uint uint_1, Account account_0)
        {
            this.uint_0 = uint_1;
            if (account_0 != null)
            {
                this.battlePassModel_0 = SeasonChallengeXML.GetActiveSeasonPass();
                this.playerBattlepass_0 = account_0.Battlepass;
                if ((this.battlePassModel_0 != null) && (this.playerBattlepass_0 != null))
                {
                    this.valueTuple_0 = this.battlePassModel_0.GetLevelProgression(this.playerBattlepass_0.TotalPoints);
                }
            }
        }

        public override void Write()
        {
            base.WriteH((short) 0x2107);
            base.WriteH((short) 0);
            base.WriteD(this.uint_0);
            if (this.uint_0 == 0)
            {
                base.WriteD((int) this.battlePassModel_0.SeasonIsEnabled());
                base.WriteC((byte) this.valueTuple_0.Item2);
                base.WriteD(this.playerBattlepass_0.TotalPoints);
                base.WriteC((byte) this.valueTuple_0.Item1);
                this.WriteC(this.playerBattlepass_0.IsPremium ? ((byte) this.valueTuple_0.Item1) : ((byte) 0));
                base.WriteC((byte) this.playerBattlepass_0.IsPremium);
                base.WriteD(1);
                base.WriteD(1);
                base.WriteD(1);
                base.WriteD(1);
                base.WriteD(1);
            }
        }
    }
}

