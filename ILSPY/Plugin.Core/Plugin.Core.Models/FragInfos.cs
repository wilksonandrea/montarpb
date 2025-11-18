using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Plugin.Core.Enums;

namespace Plugin.Core.Models;

public class FragInfos
{
	[CompilerGenerated]
	private byte byte_0;

	[CompilerGenerated]
	private byte byte_1;

	[CompilerGenerated]
	private byte byte_2;

	[CompilerGenerated]
	private byte byte_3;

	[CompilerGenerated]
	private CharaKillType charaKillType_0;

	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private float float_0;

	[CompilerGenerated]
	private float float_1;

	[CompilerGenerated]
	private float float_2;

	[CompilerGenerated]
	private List<FragModel> list_0;

	public byte KillerSlot
	{
		[CompilerGenerated]
		get
		{
			return byte_0;
		}
		[CompilerGenerated]
		set
		{
			byte_0 = value;
		}
	}

	public byte KillsCount
	{
		[CompilerGenerated]
		get
		{
			return byte_1;
		}
		[CompilerGenerated]
		set
		{
			byte_1 = value;
		}
	}

	public byte Flag
	{
		[CompilerGenerated]
		get
		{
			return byte_2;
		}
		[CompilerGenerated]
		set
		{
			byte_2 = value;
		}
	}

	public byte Unk
	{
		[CompilerGenerated]
		get
		{
			return byte_3;
		}
		[CompilerGenerated]
		set
		{
			byte_3 = value;
		}
	}

	public CharaKillType KillingType
	{
		[CompilerGenerated]
		get
		{
			return charaKillType_0;
		}
		[CompilerGenerated]
		set
		{
			charaKillType_0 = value;
		}
	}

	public int WeaponId
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

	public int Score
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

	public float X
	{
		[CompilerGenerated]
		get
		{
			return float_0;
		}
		[CompilerGenerated]
		set
		{
			float_0 = value;
		}
	}

	public float Y
	{
		[CompilerGenerated]
		get
		{
			return float_1;
		}
		[CompilerGenerated]
		set
		{
			float_1 = value;
		}
	}

	public float Z
	{
		[CompilerGenerated]
		get
		{
			return float_2;
		}
		[CompilerGenerated]
		set
		{
			float_2 = value;
		}
	}

	public List<FragModel> Frags
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

	public FragInfos()
	{
		Frags = new List<FragModel>();
	}

	public KillingMessage GetAllKillFlags()
	{
		KillingMessage killingMessage = KillingMessage.None;
		foreach (FragModel frag in Frags)
		{
			if (!killingMessage.HasFlag(frag.KillFlag))
			{
				killingMessage |= frag.KillFlag;
			}
		}
		return killingMessage;
	}
}
