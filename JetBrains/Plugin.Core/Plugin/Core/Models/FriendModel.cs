// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.FriendModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class FriendModel
{
  [CompilerGenerated]
  [SpecialName]
  public long get_PlayerId() => ((ClanInvite) this).long_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_PlayerId([In] long obj0) => ((ClanInvite) this).long_0 = obj0;

  [CompilerGenerated]
  [SpecialName]
  public string get_Text() => ((ClanInvite) this).string_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Text(string Stat) => ((ClanInvite) this).string_0 = Stat;

  public long ObjectId { get; [param: In] set; }

  public long OwnerId { get; set; }

  public long PlayerId { get; set; }

  public int State { get; set; }

  public bool Removed { get; set; }

  public PlayerInfo Info
  {
    [CompilerGenerated, SpecialName] get => ((FriendModel) this).playerInfo_0;
    [CompilerGenerated, SpecialName] set => ((FriendModel) this).playerInfo_0 = value;
  }
}
