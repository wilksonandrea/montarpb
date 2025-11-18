using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models;

public class VoteKickModel
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private int int_2;

	[CompilerGenerated]
	private int int_3;

	[CompilerGenerated]
	private int int_4;

	[CompilerGenerated]
	private int int_5;

	[CompilerGenerated]
	private int int_6;

	[CompilerGenerated]
	private List<int> list_0;

	[CompilerGenerated]
	private bool[] bool_0;

	public int CreatorIdx
	{
		[CompilerGenerated]
		get
		{
			return int_0;
		}
		[CompilerGenerated]
		set
		{
			int_0 = value;
		}
	}

	public int VictimIdx
	{
		[CompilerGenerated]
		get
		{
			return int_1;
		}
		[CompilerGenerated]
		set
		{
			int_1 = value;
		}
	}

	public int Motive
	{
		[CompilerGenerated]
		get
		{
			return int_2;
		}
		[CompilerGenerated]
		set
		{
			int_2 = value;
		}
	}

	public int Accept
	{
		[CompilerGenerated]
		get
		{
			return int_3;
		}
		[CompilerGenerated]
		set
		{
			int_3 = value;
		}
	}

	public int Denie
	{
		[CompilerGenerated]
		get
		{
			return int_4;
		}
		[CompilerGenerated]
		set
		{
			int_4 = value;
		}
	}

	public int Allies
	{
		[CompilerGenerated]
		get
		{
			return int_5;
		}
		[CompilerGenerated]
		set
		{
			int_5 = value;
		}
	}

	public int Enemies
	{
		[CompilerGenerated]
		get
		{
			return int_6;
		}
		[CompilerGenerated]
		set
		{
			int_6 = value;
		}
	}

	public List<int> Votes
	{
		[CompilerGenerated]
		get
		{
			return list_0;
		}
		[CompilerGenerated]
		set
		{
			list_0 = value;
		}
	}

	public bool[] TotalArray
	{
		[CompilerGenerated]
		get
		{
			return bool_0;
		}
		[CompilerGenerated]
		set
		{
			bool_0 = value;
		}
	}

	public VoteKickModel(int int_7, int int_8)
	{
		Accept = 1;
		Denie = 1;
		CreatorIdx = int_7;
		VictimIdx = int_8;
		Votes = new List<int>();
		Votes.Add(int_7);
		Votes.Add(int_8);
		TotalArray = new bool[18];
	}

	public int GetInGamePlayers()
	{
		int num = 0;
		for (int i = 0; i < 18; i++)
		{
			if (TotalArray[i])
			{
				num++;
			}
		}
		return num;
	}
}
