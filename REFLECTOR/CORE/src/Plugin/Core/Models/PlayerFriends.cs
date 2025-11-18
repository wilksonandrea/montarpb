namespace Plugin.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class PlayerFriends
    {
        public bool MemoryCleaned;

        public PlayerFriends()
        {
            this.Friends = new List<FriendModel>();
        }

        public void AddFriend(FriendModel friend)
        {
            List<FriendModel> friends = this.Friends;
            lock (friends)
            {
                this.Friends.Add(friend);
            }
        }

        public void CleanList()
        {
            List<FriendModel> friends = this.Friends;
            lock (friends)
            {
                using (List<FriendModel>.Enumerator enumerator = this.Friends.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        enumerator.Current.Info = null;
                    }
                }
            }
            this.MemoryCleaned = true;
        }

        public FriendModel GetFriend(int idx)
        {
            FriendModel model;
            List<FriendModel> friends = this.Friends;
            lock (friends)
            {
                try
                {
                    model = this.Friends[idx];
                }
                catch
                {
                    model = null;
                }
            }
            return model;
        }

        public FriendModel GetFriend(long id)
        {
            FriendModel model2;
            List<FriendModel> friends = this.Friends;
            lock (friends)
            {
                int num = 0;
                while (true)
                {
                    if (num < this.Friends.Count)
                    {
                        FriendModel model = this.Friends[num];
                        if (model.PlayerId != id)
                        {
                            num++;
                            continue;
                        }
                        model2 = model;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return model2;
        }

        public FriendModel GetFriend(long id, out int index)
        {
            FriendModel model2;
            List<FriendModel> friends = this.Friends;
            lock (friends)
            {
                int num = 0;
                while (true)
                {
                    if (num < this.Friends.Count)
                    {
                        FriendModel model = this.Friends[num];
                        if (model.PlayerId != id)
                        {
                            num++;
                            continue;
                        }
                        index = num;
                        model2 = model;
                    }
                    else
                    {
                        index = -1;
                        return null;
                    }
                    break;
                }
            }
            return model2;
        }

        public int GetFriendIdx(long id)
        {
            int num2;
            List<FriendModel> friends = this.Friends;
            lock (friends)
            {
                int num = 0;
                while (true)
                {
                    if (num < this.Friends.Count)
                    {
                        if (this.Friends[num].PlayerId != id)
                        {
                            num++;
                            continue;
                        }
                        num2 = num;
                    }
                    else
                    {
                        return -1;
                    }
                    break;
                }
            }
            return num2;
        }

        public bool RemoveFriend(FriendModel friend)
        {
            List<FriendModel> friends = this.Friends;
            lock (friends)
            {
                return this.Friends.Remove(friend);
            }
        }

        public void RemoveFriend(int index)
        {
            List<FriendModel> friends = this.Friends;
            lock (friends)
            {
                this.Friends.RemoveAt(index);
            }
        }

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

        public List<FriendModel> Friends { get; set; }
    }
}

