using System.Runtime.CompilerServices;
using Plugin.Core.Utility;

namespace Plugin.Core.Models;

public class PlayerInfo
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private long long_0;

	[CompilerGenerated]
	private string string_0;

	[CompilerGenerated]
	private bool bool_0;

	[CompilerGenerated]
	private AccountStatus accountStatus_0;

	public int Rank
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

	public int NickColor
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

	public long PlayerId
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

	public string Nickname
	{
		[CompilerGenerated]
		get
		{
			return string_0;
		}
		[CompilerGenerated]
		set
		{
			string_0 = value;
		}
	}

	public bool IsOnline
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

	public AccountStatus Status
	{
		[CompilerGenerated]
		get
		{
			return accountStatus_0;
		}
		[CompilerGenerated]
		set
		{
			accountStatus_0 = value;
		}
	}

	public PlayerInfo(long long_1)
	{
		PlayerId = long_1;
		Status = new AccountStatus();
	}

	public PlayerInfo(long long_1, int int_2, int int_3, string string_1, bool bool_1, AccountStatus accountStatus_1)
	{
		PlayerId = long_1;
		SetInfo(int_2, int_3, string_1, bool_1, accountStatus_1);
	}

	public void SetOnlineStatus(bool state)
	{
		if (IsOnline != state && ComDiv.UpdateDB("accounts", "online", state, "player_id", PlayerId))
		{
			IsOnline = state;
		}
	}

	public void SetInfo(int Rank, int NickColor, string Nickname, bool IsOnline, AccountStatus Status)
	{
		this.Rank = Rank;
		this.NickColor = NickColor;
		this.Nickname = Nickname;
		this.IsOnline = IsOnline;
		this.Status = Status;
	}
}
