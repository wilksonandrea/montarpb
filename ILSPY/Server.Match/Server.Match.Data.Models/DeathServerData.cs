using System.Runtime.CompilerServices;
using Plugin.Core.Enums;

namespace Server.Match.Data.Models;

public class DeathServerData
{
	[CompilerGenerated]
	private CharaDeath charaDeath_0;

	[CompilerGenerated]
	private PlayerModel playerModel_0;

	[CompilerGenerated]
	private int int_0;

	public CharaDeath DeathType
	{
		[CompilerGenerated]
		get
		{
			return charaDeath_0;
		}
		[CompilerGenerated]
		set
		{
			charaDeath_0 = value;
		}
	}

	public PlayerModel Player
	{
		[CompilerGenerated]
		get
		{
			return playerModel_0;
		}
		[CompilerGenerated]
		set
		{
			playerModel_0 = value;
		}
	}

	public int AssistSlot
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

	public DeathServerData()
	{
		AssistSlot = -1;
	}
}
