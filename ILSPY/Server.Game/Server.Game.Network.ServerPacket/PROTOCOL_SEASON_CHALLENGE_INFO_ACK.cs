using Plugin.Core.Models;
using Plugin.Core.XML;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

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
			battlePassModel_0 = SeasonChallengeXML.GetActiveSeasonPass();
			playerBattlepass_0 = account_0.Battlepass;
			if (battlePassModel_0 != null && playerBattlepass_0 != null)
			{
				valueTuple_0 = battlePassModel_0.GetLevelProgression(playerBattlepass_0.TotalPoints);
				int_0 = playerBattlepass_0.TotalPoints - valueTuple_0.Item3;
				int_1 = valueTuple_0.Item4 - valueTuple_0.Item3;
			}
		}
	}

	public override void Write()
	{
		WriteH(8450);
		WriteH(0);
		if (battlePassModel_0 != null)
		{
			WriteD(battlePassModel_0.SeasonIsEnabled() ? 1 : 0);
			WriteC((byte)valueTuple_0.Item2);
			WriteD(playerBattlepass_0.TotalPoints);
			WriteC((byte)valueTuple_0.Item1);
			WriteC((byte)(playerBattlepass_0.IsPremium ? ((uint)valueTuple_0.Item1) : 0u));
			WriteC(playerBattlepass_0.IsPremium ? ((byte)1) : ((byte)0));
			WriteC(0);
			WriteD(playerBattlepass_0.LastRecord);
			WriteD(playerBattlepass_0.DailyPoints);
			WriteD(1);
			WriteC((byte)((!battlePassModel_0.IsCompleted(playerBattlepass_0.TotalPoints)) ? (battlePassModel_0.Enable ? 1u : 2u) : 0u));
			WriteU(battlePassModel_0.Name, 42);
			WriteH((short)battlePassModel_0.GetCardCount());
			WriteD(battlePassModel_0.MaxDailyPoints);
			WriteD(0);
			for (int i = 0; i < 99; i++)
			{
				PassBoxModel passBoxModel = battlePassModel_0.Cards[i];
				WriteD(passBoxModel.Normal.GoodId);
				WriteD(passBoxModel.PremiumA.GoodId);
				WriteD(passBoxModel.PremiumB.GoodId);
				WriteD(passBoxModel.RequiredPoints);
			}
			WriteD(0);
			WriteD(0);
			WriteD(0);
			WriteD(battlePassModel_0.BeginDate);
			WriteD(battlePassModel_0.EndedDate);
			WriteD(int_0);
			WriteD(int_1);
		}
	}
}
