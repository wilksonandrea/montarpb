using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class PlayerFriends
	{
		public bool MemoryCleaned;

		public List<FriendModel> Friends
		{
			get;
			set;
		}

		public PlayerFriends()
		{
			this.Friends = new List<FriendModel>();
		}

		public void AddFriend(FriendModel friend)
		{
			lock (this.Friends)
			{
				this.Friends.Add(friend);
			}
		}

		public void CleanList()
		{
			lock (this.Friends)
			{
				foreach (FriendModel friend in this.Friends)
				{
					friend.Info = null;
				}
			}
			this.MemoryCleaned = true;
		}

		public FriendModel GetFriend(int idx)
		{
			FriendModel ıtem;
			lock (this.Friends)
			{
				try
				{
					ıtem = this.Friends[idx];
				}
				catch
				{
					ıtem = null;
				}
			}
			return ıtem;
		}

		public FriendModel GetFriend(long id)
		{
			FriendModel friendModel;
			lock (this.Friends)
			{
				int ınt32 = 0;
				while (ınt32 < this.Friends.Count)
				{
					FriendModel ıtem = this.Friends[ınt32];
					if (ıtem.PlayerId != id)
					{
						ınt32++;
					}
					else
					{
						friendModel = ıtem;
						return friendModel;
					}
				}
				return null;
			}
			return friendModel;
		}

		public FriendModel GetFriend(long id, out int index)
		{
			FriendModel friendModel;
			lock (this.Friends)
			{
				int ınt32 = 0;
				while (ınt32 < this.Friends.Count)
				{
					FriendModel ıtem = this.Friends[ınt32];
					if (ıtem.PlayerId != id)
					{
						ınt32++;
					}
					else
					{
						index = ınt32;
						friendModel = ıtem;
						return friendModel;
					}
				}
				index = -1;
				return null;
			}
			return friendModel;
		}

		public int GetFriendIdx(long id)
		{
			int ınt32;
			lock (this.Friends)
			{
				int ınt321 = 0;
				while (ınt321 < this.Friends.Count)
				{
					if (this.Friends[ınt321].PlayerId != id)
					{
						ınt321++;
					}
					else
					{
						ınt32 = ınt321;
						return ınt32;
					}
				}
				return -1;
			}
			return ınt32;
		}

		public bool RemoveFriend(FriendModel friend)
		{
			bool flag;
			lock (this.Friends)
			{
				flag = this.Friends.Remove(friend);
			}
			return flag;
		}

		public void RemoveFriend(int index)
		{
			lock (this.Friends)
			{
				this.Friends.RemoveAt(index);
			}
		}

		public void RemoveFriend(long id)
		{
			lock (this.Friends)
			{
				int ınt32 = 0;
				while (ınt32 < this.Friends.Count)
				{
					if (this.Friends[ınt32].PlayerId != id)
					{
						ınt32++;
					}
					else
					{
						this.Friends.RemoveAt(ınt32);
						return;
					}
				}
			}
		}
	}
}