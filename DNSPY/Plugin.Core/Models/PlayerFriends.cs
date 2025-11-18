using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x0200008A RID: 138
	public class PlayerFriends
	{
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00005890 File Offset: 0x00003A90
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x00005898 File Offset: 0x00003A98
		public List<FriendModel> Friends
		{
			[CompilerGenerated]
			get
			{
				return this.list_0;
			}
			[CompilerGenerated]
			set
			{
				this.list_0 = value;
			}
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x000058A1 File Offset: 0x00003AA1
		public PlayerFriends()
		{
			this.Friends = new List<FriendModel>();
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0001BC24 File Offset: 0x00019E24
		public void CleanList()
		{
			List<FriendModel> friends = this.Friends;
			lock (friends)
			{
				foreach (FriendModel friendModel in this.Friends)
				{
					friendModel.Info = null;
				}
			}
			this.MemoryCleaned = true;
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001BCA4 File Offset: 0x00019EA4
		public void AddFriend(FriendModel friend)
		{
			List<FriendModel> friends = this.Friends;
			lock (friends)
			{
				this.Friends.Add(friend);
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001BCEC File Offset: 0x00019EEC
		public bool RemoveFriend(FriendModel friend)
		{
			List<FriendModel> friends = this.Friends;
			bool flag2;
			lock (friends)
			{
				flag2 = this.Friends.Remove(friend);
			}
			return flag2;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001BD34 File Offset: 0x00019F34
		public void RemoveFriend(int index)
		{
			List<FriendModel> friends = this.Friends;
			lock (friends)
			{
				this.Friends.RemoveAt(index);
			}
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001BD7C File Offset: 0x00019F7C
		public void RemoveFriend(long id)
		{
			List<FriendModel> friends = this.Friends;
			lock (friends)
			{
				for (int i = 0; i < this.Friends.Count; i++)
				{
					if (this.Friends[i].PlayerId == id)
					{
						this.Friends.RemoveAt(i);
						break;
					}
				}
			}
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001BDF0 File Offset: 0x00019FF0
		public int GetFriendIdx(long id)
		{
			List<FriendModel> friends = this.Friends;
			lock (friends)
			{
				for (int i = 0; i < this.Friends.Count; i++)
				{
					if (this.Friends[i].PlayerId == id)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001BE60 File Offset: 0x0001A060
		public FriendModel GetFriend(int idx)
		{
			List<FriendModel> friends = this.Friends;
			FriendModel friendModel;
			lock (friends)
			{
				try
				{
					friendModel = this.Friends[idx];
				}
				catch
				{
					friendModel = null;
				}
			}
			return friendModel;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0001BEBC File Offset: 0x0001A0BC
		public FriendModel GetFriend(long id)
		{
			List<FriendModel> friends = this.Friends;
			lock (friends)
			{
				for (int i = 0; i < this.Friends.Count; i++)
				{
					FriendModel friendModel = this.Friends[i];
					if (friendModel.PlayerId == id)
					{
						return friendModel;
					}
				}
			}
			return null;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0001BF30 File Offset: 0x0001A130
		public FriendModel GetFriend(long id, out int index)
		{
			List<FriendModel> friends = this.Friends;
			lock (friends)
			{
				for (int i = 0; i < this.Friends.Count; i++)
				{
					FriendModel friendModel = this.Friends[i];
					if (friendModel.PlayerId == id)
					{
						index = i;
						return friendModel;
					}
				}
			}
			index = -1;
			return null;
		}

		// Token: 0x0400029A RID: 666
		[CompilerGenerated]
		private List<FriendModel> list_0;

		// Token: 0x0400029B RID: 667
		public bool MemoryCleaned;
	}
}
