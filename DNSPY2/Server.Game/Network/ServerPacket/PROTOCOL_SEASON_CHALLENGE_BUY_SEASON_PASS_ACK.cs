using System;
using Plugin.Core.Models;
using Plugin.Core.XML;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000107 RID: 263
	public class PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK : GameServerPacket
	{
		// Token: 0x06000283 RID: 643 RVA: 0x00013B08 File Offset: 0x00011D08
		public PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK(uint uint_1, Account account_0)
		{
			this.uint_0 = uint_1;
			if (account_0 != null)
			{
				this.battlePassModel_0 = SeasonChallengeXML.GetActiveSeasonPass();
				this.playerBattlepass_0 = account_0.Battlepass;
				if (this.battlePassModel_0 != null && this.playerBattlepass_0 != null)
				{
					this.valueTuple_0 = this.battlePassModel_0.GetLevelProgression(this.playerBattlepass_0.TotalPoints);
				}
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00013B68 File Offset: 0x00011D68
		public override void Write()
		{
			base.WriteH(8455);
			base.WriteH(0);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0U)
			{
				base.WriteD((this.battlePassModel_0.SeasonIsEnabled() > false) ? 1 : 0);
				base.WriteC((byte)this.valueTuple_0.Item2);
				base.WriteD(this.playerBattlepass_0.TotalPoints);
				base.WriteC((byte)this.valueTuple_0.Item1);
				base.WriteC((byte)(this.playerBattlepass_0.IsPremium ? this.valueTuple_0.Item1 : 0));
				base.WriteC((this.playerBattlepass_0.IsPremium > false) ? 1 : 0);
				base.WriteD(1);
				base.WriteD(1);
				base.WriteD(1);
				base.WriteD(1);
				base.WriteD(1);
			}
		}

		// Token: 0x040001E2 RID: 482
		private readonly uint uint_0;

		// Token: 0x040001E3 RID: 483
		private readonly BattlePassModel battlePassModel_0;

		// Token: 0x040001E4 RID: 484
		private readonly PlayerBattlepass playerBattlepass_0;

		// Token: 0x040001E5 RID: 485
		private readonly ValueTuple<int, int, int, int> valueTuple_0;
	}
}
