namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Plugin.Core.XML;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_SEASON_CHALLENGE_INFO_ACK : GameServerPacket
    {
        private readonly BattlePassModel battlePassModel_0;
        private readonly PlayerBattlepass playerBattlepass_0;
        private readonly (int, int, int, int) valueTuple_0;
        private readonly int int_0;
        private readonly int int_1;

        public PROTOCOL_SEASON_CHALLENGE_INFO_ACK(Account account_0)
        {
            if (account_0 != null)
            {
                this.battlePassModel_0 = SeasonChallengeXML.GetActiveSeasonPass();
                this.playerBattlepass_0 = account_0.Battlepass;
                if ((this.battlePassModel_0 != null) && (this.playerBattlepass_0 != null))
                {
                    this.valueTuple_0 = this.battlePassModel_0.GetLevelProgression(this.playerBattlepass_0.TotalPoints);
                    this.int_0 = this.playerBattlepass_0.TotalPoints - this.valueTuple_0.Item3;
                    this.int_1 = this.valueTuple_0.Item4 - this.valueTuple_0.Item3;
                }
            }
        }

        public override void Write()
        {
            base.WriteH((short) 0x2102);
            base.WriteH((short) 0);
            if (this.battlePassModel_0 != null)
            {
                base.WriteD((int) this.battlePassModel_0.SeasonIsEnabled());
                base.WriteC((byte) this.valueTuple_0.Item2);
                base.WriteD(this.playerBattlepass_0.TotalPoints);
                base.WriteC((byte) this.valueTuple_0.Item1);
                this.WriteC(this.playerBattlepass_0.IsPremium ? ((byte) this.valueTuple_0.Item1) : ((byte) 0));
                base.WriteC((byte) this.playerBattlepass_0.IsPremium);
                base.WriteC(0);
                base.WriteD(this.playerBattlepass_0.LastRecord);
                base.WriteD(this.playerBattlepass_0.DailyPoints);
                base.WriteD(1);
                this.WriteC(this.battlePassModel_0.IsCompleted(this.playerBattlepass_0.TotalPoints) ? ((byte) 0) : (this.battlePassModel_0.Enable ? ((byte) 1) : ((byte) 2)));
                base.WriteU(this.battlePassModel_0.Name, 0x2a);
                base.WriteH((short) this.battlePassModel_0.GetCardCount());
                base.WriteD(this.battlePassModel_0.MaxDailyPoints);
                base.WriteD(0);
                int num = 0;
                while (true)
                {
                    if (num >= 0x63)
                    {
                        base.WriteD(0);
                        base.WriteD(0);
                        base.WriteD(0);
                        base.WriteD(this.battlePassModel_0.BeginDate);
                        base.WriteD(this.battlePassModel_0.EndedDate);
                        base.WriteD(this.int_0);
                        base.WriteD(this.int_1);
                        break;
                    }
                    PassBoxModel model = this.battlePassModel_0.Cards[num];
                    base.WriteD(model.Normal.GoodId);
                    base.WriteD(model.PremiumA.GoodId);
                    base.WriteD(model.PremiumB.GoodId);
                    base.WriteD(model.RequiredPoints);
                    num++;
                }
            }
        }
    }
}

