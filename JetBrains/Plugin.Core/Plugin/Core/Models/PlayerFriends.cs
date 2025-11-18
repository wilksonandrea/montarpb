// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.PlayerFriends
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class PlayerFriends
{
  public bool MemoryCleaned;

  [CompilerGenerated]
  [SpecialName]
  public uint get_CreateDate() => ((CharacterModel) this).uint_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_CreateDate([In] uint obj0) => ((CharacterModel) this).uint_0 = obj0;

  [CompilerGenerated]
  [SpecialName]
  public uint get_PlayTime() => ((CharacterModel) this).uint_1;

  [CompilerGenerated]
  [SpecialName]
  public void set_PlayTime(uint value) => ((CharacterModel) this).uint_1 = value;

  public PlayerFriends() => ((CharacterModel) this).Name = "";

  public List<FriendModel> Friends { get; set; }

  public PlayerFriends() => this.Friends = new List<FriendModel>();

  public void CleanList()
  {
    lock (this.Friends)
    {
      foreach (MessageModel friend in this.Friends)
        friend.set_Info((PlayerInfo) null);
    }
    this.MemoryCleaned = true;
  }

  public void AddFriend(FriendModel value)
  {
    lock (this.Friends)
      this.Friends.Add(value);
  }

  public bool RemoveFriend(FriendModel value)
  {
    lock (this.Friends)
      return this.Friends.Remove(value);
  }

  public void RemoveFriend(int value)
  {
    lock (this.Friends)
      this.Friends.RemoveAt(value);
  }
}
