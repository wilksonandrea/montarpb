// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.MessageModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class MessageModel
{
  [CompilerGenerated]
  [SpecialName]
  public PlayerInfo get_Info() => ((FriendModel) this).playerInfo_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Info(PlayerInfo value) => ((FriendModel) this).playerInfo_0 = value;

  public MessageModel(long value)
  {
    ((FriendModel) this).PlayerId = value;
    this.set_Info((PlayerInfo) new PlayerInventory(value));
  }

  public MessageModel(
    long value,
    [In] int obj1,
    [In] int obj2,
    [In] string obj3,
    [In] bool obj4,
    [In] AccountStatus obj5)
  {
    ((FriendModel) this).PlayerId = value;
    this.SetModel(value, obj1, obj2, obj3, obj4, obj5);
  }

  public void SetModel(
    [In] long obj0,
    int int_1,
    int int_2,
    string string_0,
    bool bool_1,
    AccountStatus accountStatus_0)
  {
    this.set_Info((PlayerInfo) new PlayerInventory(obj0, int_1, int_2, string_0, bool_1, accountStatus_0));
  }

  public int ClanId { get; [param: In] set; }

  public long ObjectId { get; [param: In] set; }

  public long SenderId { get; [param: In] set; }

  public long ExpireDate { get; [param: In] set; }

  public string SenderName { get; [param: In] set; }

  public string Text { get; set; }

  public NoteMessageState State { get; set; }

  public NoteMessageType Type { get; set; }

  public NoteMessageClan ClanNote { get; set; }

  public int DaysRemaining { get; set; }
}
