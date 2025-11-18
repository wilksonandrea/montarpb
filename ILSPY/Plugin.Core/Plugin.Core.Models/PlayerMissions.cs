using System.Runtime.CompilerServices;
using Plugin.Core.Utility;

namespace Plugin.Core.Models;

public class PlayerMissions
{
	[CompilerGenerated]
	private long long_0;

	[CompilerGenerated]
	private byte[] byte_0;

	[CompilerGenerated]
	private byte[] byte_1;

	[CompilerGenerated]
	private byte[] byte_2;

	[CompilerGenerated]
	private byte[] byte_3;

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
	private int int_7;

	[CompilerGenerated]
	private int int_8;

	[CompilerGenerated]
	private bool bool_0;

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

	public byte[] List1
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

	public byte[] List2
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

	public byte[] List3
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

	public byte[] List4
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

	public int ActualMission
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

	public int Card1
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

	public int Card2
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

	public int Card3
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

	public int Card4
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

	public int Mission1
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

	public int Mission2
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

	public int Mission3
	{
		[CompilerGenerated]
		get
		{
			return int_7;
		}
		[CompilerGenerated]
		set
		{
			int_7 = value;
		}
	}

	public int Mission4
	{
		[CompilerGenerated]
		get
		{
			return int_8;
		}
		[CompilerGenerated]
		set
		{
			int_8 = value;
		}
	}

	public bool SelectedCard
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

	public PlayerMissions()
	{
		List1 = new byte[40];
		List2 = new byte[40];
		List3 = new byte[40];
		List4 = new byte[40];
	}

	public PlayerMissions DeepCopy()
	{
		return MemberwiseClone() as PlayerMissions;
	}

	public byte[] GetCurrentMissionList()
	{
		if (ActualMission == 0)
		{
			return List1;
		}
		if (ActualMission == 1)
		{
			return List2;
		}
		if (ActualMission == 2)
		{
			return List3;
		}
		return List4;
	}

	public int GetCurrentCard()
	{
		return GetCard(ActualMission);
	}

	public int GetCard(int Index)
	{
		return Index switch
		{
			0 => Card1, 
			1 => Card2, 
			2 => Card3, 
			_ => Card4, 
		};
	}

	public int GetCurrentMissionId()
	{
		if (ActualMission == 0)
		{
			return Mission1;
		}
		if (ActualMission == 1)
		{
			return Mission2;
		}
		if (ActualMission == 2)
		{
			return Mission3;
		}
		return Mission4;
	}

	public void UpdateSelectedCard()
	{
		int currentCard = GetCurrentCard();
		if (ushort.MaxValue == ComDiv.GetMissionCardFlags(GetCurrentMissionId(), currentCard, GetCurrentMissionList()))
		{
			SelectedCard = true;
		}
	}
}
