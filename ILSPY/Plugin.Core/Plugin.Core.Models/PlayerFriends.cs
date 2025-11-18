using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models;

public class PlayerFriends
{
	[CompilerGenerated]
	private List<FriendModel> list_0;

	public bool MemoryCleaned;

	public List<FriendModel> Friends
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

	public PlayerFriends()
	{
		Friends = new List<FriendModel>();
	}

	public void CleanList()
	{
		lock (Friends)
		{
			foreach (FriendModel friend in Friends)
			{
				friend.Info = null;
			}
		}
		MemoryCleaned = true;
	}

	public void AddFriend(FriendModel friend)
	{
		lock (Friends)
		{
			Friends.Add(friend);
		}
	}

	public bool RemoveFriend(FriendModel friend)
	{
		lock (Friends)
		{
			return Friends.Remove(friend);
		}
	}

	public void RemoveFriend(int index)
	{
		lock (Friends)
		{
			Friends.RemoveAt(index);
		}
	}

	public void RemoveFriend(long id)
	{
		lock (Friends)
		{
			int num = 0;
			while (true)
			{
				if (num < Friends.Count)
				{
					if (Friends[num].PlayerId == id)
					{
						break;
					}
					num++;
					continue;
				}
				return;
			}
			Friends.RemoveAt(num);
		}
	}

	public int GetFriendIdx(long id)
	{
		lock (Friends)
		{
			for (int i = 0; i < Friends.Count; i++)
			{
				if (Friends[i].PlayerId == id)
				{
					return i;
				}
			}
		}
		return -1;
	}

	public FriendModel GetFriend(int idx)
	{
		lock (Friends)
		{
			try
			{
				return Friends[idx];
			}
			catch
			{
				return null;
			}
		}
	}

	public FriendModel GetFriend(long id)
	{
		lock (Friends)
		{
			for (int i = 0; i < Friends.Count; i++)
			{
				FriendModel friendModel = Friends[i];
				if (friendModel.PlayerId == id)
				{
					return friendModel;
				}
			}
		}
		return null;
	}

	public FriendModel GetFriend(long id, out int index)
	{
		lock (Friends)
		{
			for (int i = 0; i < Friends.Count; i++)
			{
				FriendModel friendModel = Friends[i];
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
}
