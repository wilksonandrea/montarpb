using System;
using Plugin.Core.Models;
using Plugin.Core.XML;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000108 RID: 264
	public class PROTOCOL_SEASON_CHALLENGE_INFO_ACK : GameServerPacket
	{
		// Token: 0x06000285 RID: 645 RVA: 0x00013C44 File Offset: 0x00011E44
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

		// Token: 0x06000286 RID: 646 RVA: 0x00013CD8 File Offset: 0x00011ED8
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

		// Token: 0x040001E6 RID: 486
		private readonly BattlePassModel battlePassModel_0;

		// Token: 0x040001E7 RID: 487
		private readonly PlayerBattlepass playerBattlepass_0;

		// Token: 0x040001E8 RID: 488
		private readonly ValueTuple<int, int, int, int> valueTuple_0;

		// Token: 0x040001E9 RID: 489
		private readonly int int_0;

		// Token: 0x040001EA RID: 490
		private readonly int int_1;
	}
}
