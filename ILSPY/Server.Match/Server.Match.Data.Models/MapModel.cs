using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Server.Match.Data.Models;

public class MapModel
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private List<ObjectModel> list_0;

	[CompilerGenerated]
	private List<BombPosition> list_1;

	public int Id
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

	public List<ObjectModel> Objects
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

	public List<BombPosition> Bombs
	{
		[CompilerGenerated]
		get
		{
			return list_1;
		}
		[CompilerGenerated]
		set
		{
			list_1 = value;
		}
	}

	public BombPosition GetBomb(int BombId)
	{
		try
		{
			return Bombs[BombId];
		}
		catch
		{
			return null;
		}
	}
}
