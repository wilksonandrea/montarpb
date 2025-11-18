using Plugin.Core.Utility;
using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class PlayerInfo
	{
		public bool IsOnline
		{
			get;
			set;
		}

		public int NickColor
		{
			get;
			set;
		}

		public string Nickname
		{
			get;
			set;
		}

		public long PlayerId
		{
			get;
			set;
		}

		public int Rank
		{
			get;
			set;
		}

		public AccountStatus Status
		{
			get;
			set;
		}

		public PlayerInfo(long long_1)
		{
			this.PlayerId = long_1;
			this.Status = new AccountStatus();
		}

		public PlayerInfo(long long_1, int int_2, int int_3, string string_1, bool bool_1, AccountStatus accountStatus_1)
		{
			this.PlayerId = long_1;
			this.SetInfo(int_2, int_3, string_1, bool_1, accountStatus_1);
		}

		public void SetInfo(int Rank, int NickColor, string Nickname, bool IsOnline, AccountStatus Status)
		{
			this.Rank = Rank;
			this.NickColor = NickColor;
			this.Nickname = Nickname;
			this.IsOnline = IsOnline;
			this.Status = Status;
		}

		public void SetOnlineStatus(bool state)
		{
			if (this.IsOnline != state && ComDiv.UpdateDB("accounts", "online", state, "player_id", this.PlayerId))
			{
				this.IsOnline = state;
			}
		}
	}
}