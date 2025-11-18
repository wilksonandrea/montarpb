// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.ItemsModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class ItemsModel
{
  public void RemoveFriend(long value)
  {
    lock (((PlayerFriends) this).Friends)
    {
      for (int index = 0; index < ((PlayerFriends) this).Friends.Count; ++index)
      {
        if (((PlayerFriends) this).Friends[index].PlayerId == value)
        {
          ((PlayerFriends) this).Friends.RemoveAt(index);
          break;
        }
      }
    }
  }

  public int GetFriendIdx(long value)
  {
    lock (((PlayerFriends) this).Friends)
    {
      for (int index = 0; index < ((PlayerFriends) this).Friends.Count; ++index)
      {
        if (((PlayerFriends) this).Friends[index].PlayerId == value)
          return index;
      }
    }
    return -1;
  }

  public FriendModel GetFriend(int friend)
  {
    lock (((PlayerFriends) this).Friends)
    {
      try
      {
        return ((PlayerFriends) this).Friends[friend];
      }
      catch
      {
        return (FriendModel) null;
      }
    }
  }

  public FriendModel GetFriend(long friend)
  {
    lock (((PlayerFriends) this).Friends)
    {
      for (int index = 0; index < ((PlayerFriends) this).Friends.Count; ++index)
      {
        FriendModel friend1 = ((PlayerFriends) this).Friends[index];
        if (friend1.PlayerId == friend)
          return friend1;
      }
    }
    return (FriendModel) null;
  }

  public FriendModel GetFriend(long index, [In] ref int obj1)
  {
    lock (((PlayerFriends) this).Friends)
    {
      for (int index1 = 0; index1 < ((PlayerFriends) this).Friends.Count; ++index1)
      {
        FriendModel friend = ((PlayerFriends) this).Friends[index1];
        if (friend.PlayerId == index)
        {
          obj1 = index1;
          return friend;
        }
      }
    }
    obj1 = -1;
    return (FriendModel) null;
  }

  public int Id { get; set; }

  public string Name { get; set; }

  public ItemCategory Category { get; set; }

  public ItemEquipType Equip { get; set; }

  public long ObjectId { get; [param: In] set; }

  public uint Count { get; set; }
}
