using System;
using Plugin.Core.Models;
using Plugin.Core.XML;
using Server.Auth.Data.Models;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x02000013 RID: 19
	public class PROTOCOL_SEASON_CHALLENGE_INFO_ACK : AuthServerPacket
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00004454 File Offset: 0x00002654
		public PROTOCOL_SEASON_CHALLENGE_INFO_ACK(Account account_0)
		{
			if (account_0 != null)
			{
				this.battlePassModel_0 = SeasonChallengeXML.GetActiveSeasonPass();
				this.playerBattlepass_0 = account_0.Battlepass;
				if (this.battlePassModel_0 != null && this.playerBattlepass_0 != null)
				{
					this.valueTuple_0 = this.battlePassModel_0.GetLevelProgression(this.playerBattlepass_0.TotalPoints);
					this.int_0 = this.playerBattlepass_0.TotalPoints - this.valueTuple_0.Item3;
					this.int_1 = this.valueTuple_0.Item4 - this.valueTuple_0.Item3;
				}
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000044E8 File Offset: 0x000026E8
		public override void Write()
		{
			base.WriteH(8450);
			base.WriteH(0);
			if (this.battlePassModel_0 != null)
			{
				base.WriteD((this.battlePassModel_0.SeasonIsEnabled() > false) ? 1 : 0);
				base.WriteC((byte)this.valueTuple_0.Item2);
				base.WriteD(this.playerBattlepass_0.TotalPoints);
				base.WriteC((byte)this.valueTuple_0.Item1);
				base.WriteC((byte)(this.playerBattlepass_0.IsPremium ? this.valueTuple_0.Item1 : 0));
				base.WriteC((this.playerBattlepass_0.IsPremium > false) ? 1 : 0);
				base.WriteC(0);
				base.WriteD(this.playerBattlepass_0.LastRecord);
				base.WriteD(this.playerBattlepass_0.DailyPoints);
				base.WriteD(1);
				base.WriteC(this.battlePassModel_0.IsCompleted(this.playerBattlepass_0.TotalPoints) ? 0 : (this.battlePassModel_0.Enable ? 1 : 2));
				base.WriteU(this.battlePassModel_0.Name, 42);
				base.WriteH((short)this.battlePassModel_0.GetCardCount());
				base.WriteD(this.battlePassModel_0.MaxDailyPoints);
				base.WriteD(0);
				for (int i = 0; i < 99; i++)
				{
					PassBoxModel passBoxModel = this.battlePassModel_0.Cards[i];
					base.WriteD(passBoxModel.Normal.GoodId);
					base.WriteD(passBoxModel.PremiumA.GoodId);
					base.WriteD(passBoxModel.PremiumB.GoodId);
					base.WriteD(passBoxModel.RequiredPoints);
				}
				base.WriteD(0);
				base.WriteD(0);
				base.WriteD(0);
				base.WriteD(this.battlePassModel_0.BeginDate);
				base.WriteD(this.battlePassModel_0.EndedDate);
				base.WriteD(this.int_0);
				base.WriteD(this.int_1);
			}
		}

		// Token: 0x0400002A RID: 42
		private readonly BattlePassModel battlePassModel_0;

		// Token: 0x0400002B RID: 43
		private readonly PlayerBattlepass playerBattlepass_0;

		// Token: 0x0400002C RID: 44
		private readonly ValueTuple<int, int, int, int> valueTuple_0;

		// Token: 0x0400002D RID: 45
		private readonly int int_0;

		// Token: 0x0400002E RID: 46
		private readonly int int_1;
	}
}
