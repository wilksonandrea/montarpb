using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_SEASON_CHALLENGE_INFO_ACK : GameServerPacket
	{
		private readonly BattlePassModel battlePassModel_0;

		private readonly PlayerBattlepass playerBattlepass_0;

		private readonly ValueTuple<int, int, int, int> valueTuple_0;

		private readonly int int_0;

		private readonly int int_1;

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

		public override void Write()
		{
			object ıtem1;
			object obj;
			base.WriteH(8450);
			base.WriteH(0);
			if (this.battlePassModel_0 != null)
			{
				base.WriteD(this.battlePassModel_0.SeasonIsEnabled());
				base.WriteC((byte)this.valueTuple_0.Item2);
				base.WriteD(this.playerBattlepass_0.TotalPoints);
				base.WriteC((byte)this.valueTuple_0.Item1);
				if (this.playerBattlepass_0.IsPremium)
				{
					ıtem1 = this.valueTuple_0.Item1;
				}
				else
				{
					ıtem1 = null;
				}
				base.WriteC((byte)ıtem1);
				base.WriteC((byte)this.playerBattlepass_0.IsPremium);
				base.WriteC(0);
				base.WriteD(this.playerBattlepass_0.LastRecord);
				base.WriteD(this.playerBattlepass_0.DailyPoints);
				base.WriteD(1);
				if (this.battlePassModel_0.IsCompleted(this.playerBattlepass_0.TotalPoints))
				{
					obj = null;
				}
				else
				{
					obj = (this.battlePassModel_0.Enable ? 1 : 2);
				}
				base.WriteC((byte)obj);
				base.WriteU(this.battlePassModel_0.Name, 42);
				base.WriteH((short)this.battlePassModel_0.GetCardCount());
				base.WriteD(this.battlePassModel_0.MaxDailyPoints);
				base.WriteD(0);
				for (int i = 0; i < 99; i++)
				{
					PassBoxModel ıtem = this.battlePassModel_0.Cards[i];
					base.WriteD(ıtem.Normal.GoodId);
					base.WriteD(ıtem.PremiumA.GoodId);
					base.WriteD(ıtem.PremiumB.GoodId);
					base.WriteD(ıtem.RequiredPoints);
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
	}
}