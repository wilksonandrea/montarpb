using Plugin.Core.Models;
using Plugin.Core.XML;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK : GameServerPacket
{
	private readonly uint uint_0;

	private readonly BattlePassModel battlePassModel_0;

	private readonly PlayerBattlepass playerBattlepass_0;

	private readonly (int, int, int, int) valueTuple_0;

	public PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK(uint uint_1, Account account_0)
	{
		uint_0 = uint_1;
		if (account_0 != null)
		{
			battlePassModel_0 = SeasonChallengeXML.GetActiveSeasonPass();
			playerBattlepass_0 = account_0.Battlepass;
			if (battlePassModel_0 != null && playerBattlepass_0 != null)
			{
				valueTuple_0 = battlePassModel_0.GetLevelProgression(playerBattlepass_0.TotalPoints);
			}
		}
	}

	public override void Write()
	{
		WriteH(8455);
		WriteH(0);
		WriteD(uint_0);
		if (uint_0 == 0)
		{
			WriteD(battlePassModel_0.SeasonIsEnabled() ? 1 : 0);
			WriteC((byte)valueTuple_0.Item2);
			WriteD(playerBattlepass_0.TotalPoints);
			WriteC((byte)valueTuple_0.Item1);
			WriteC((byte)(playerBattlepass_0.IsPremium ? ((uint)valueTuple_0.Item1) : 0u));
			WriteC(playerBattlepass_0.IsPremium ? ((byte)1) : ((byte)0));
			WriteD(1);
			WriteD(1);
			WriteD(1);
			WriteD(1);
			WriteD(1);
		}
	}
}
