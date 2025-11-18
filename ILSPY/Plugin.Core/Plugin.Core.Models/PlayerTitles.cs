using System.Runtime.CompilerServices;

namespace Plugin.Core.Models;

public class PlayerTitles
{
	[CompilerGenerated]
	private long long_0;

	[CompilerGenerated]
	private long long_1;

	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private int int_2;

	[CompilerGenerated]
	private int int_3;

	public long OwnerId
	{
		[CompilerGenerated]
		get
		{
			return long_0;
		}
		[CompilerGenerated]
		set
		{
			long_0 = value;
		}
	}

	public long Flags
	{
		[CompilerGenerated]
		get
		{
			return long_1;
		}
		[CompilerGenerated]
		set
		{
			long_1 = value;
		}
	}

	public int Equiped1
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

	public int Equiped2
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

	public int Equiped3
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

	public int Slots
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

	public PlayerTitles()
	{
		Slots = 1;
	}

	public long Add(long flag)
	{
		Flags |= flag;
		return Flags;
	}

	public bool Contains(long flag)
	{
		if ((Flags & flag) != flag)
		{
			return flag == 0L;
		}
		return true;
	}

	public void SetEquip(int index, int value)
	{
		switch (index)
		{
		case 0:
			Equiped1 = value;
			break;
		case 1:
			Equiped2 = value;
			break;
		case 2:
			Equiped3 = value;
			break;
		}
	}

	public int GetEquip(int index)
	{
		return index switch
		{
			0 => Equiped1, 
			1 => Equiped2, 
			2 => Equiped3, 
			_ => 0, 
		};
	}
}
