using Plugin.Core.Enums;
using Plugin.Core.SQL;

namespace Plugin.Core.Models;

public class ClanModel
{
	public int Id;

	public int Matches;

	public int MatchWins;

	public int MatchLoses;

	public int TotalKills;

	public int TotalHeadshots;

	public int TotalDeaths;

	public int TotalAssists;

	public int TotalEscapes;

	public int Authority;

	public int RankLimit;

	public int MinAgeLimit;

	public int MaxAgeLimit;

	public int Exp;

	public int Rank;

	public int NameColor;

	public int MaxPlayers;

	public int Effect;

	public string Name;

	public string Info;

	public string News;

	public long OwnerId;

	public uint Logo;

	public uint CreationDate;

	public float Points;

	public JoinClanType JoinType;

	public ClanBestPlayers BestPlayers = new ClanBestPlayers();

	public ClanModel()
	{
		MaxPlayers = 50;
		Logo = uint.MaxValue;
		Name = "";
		Info = "";
		News = "";
		Points = 1000f;
	}

	public int GetClanUnit()
	{
		return GetClanUnit(DaoManagerSQL.GetClanPlayers(Id));
	}

	public int GetClanUnit(int Count)
	{
		if (Count >= 250)
		{
			return 7;
		}
		if (Count >= 200)
		{
			return 6;
		}
		if (Count >= 150)
		{
			return 5;
		}
		if (Count >= 100)
		{
			return 4;
		}
		if (Count >= 50)
		{
			return 3;
		}
		if (Count >= 30)
		{
			return 2;
		}
		if (Count >= 10)
		{
			return 1;
		}
		return 0;
	}
}
