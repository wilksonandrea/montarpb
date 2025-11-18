using System.Runtime.CompilerServices;

namespace Plugin.Core.Models;

public class FriendModel
{
	[CompilerGenerated]
	private long long_0;

	[CompilerGenerated]
	private long long_1;

	[CompilerGenerated]
	private long long_2;

	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private bool bool_0;

	[CompilerGenerated]
	private PlayerInfo playerInfo_0;

	public long ObjectId
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

	public long OwnerId
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

	public long PlayerId
	{
		[CompilerGenerated]
		get
		{
			return long_2;
		}
		[CompilerGenerated]
		set
		{
			long_2 = value;
		}
	}

	public int State
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

	public bool Removed
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

	public PlayerInfo Info
	{
		[CompilerGenerated]
		get
		{
			return playerInfo_0;
		}
		[CompilerGenerated]
		set
		{
			playerInfo_0 = value;
		}
	}

	public FriendModel(long long_3)
	{
		PlayerId = long_3;
		Info = new PlayerInfo(long_3);
	}

	public FriendModel(long long_3, int int_1, int int_2, string string_0, bool bool_1, AccountStatus accountStatus_0)
	{
		PlayerId = long_3;
		SetModel(long_3, int_1, int_2, string_0, bool_1, accountStatus_0);
	}

	public void SetModel(long PlayerId, int Rank, int NickColor, string Nickname, bool IsOnline, AccountStatus Status)
	{
		Info = new PlayerInfo(PlayerId, Rank, NickColor, Nickname, IsOnline, Status);
	}
}
