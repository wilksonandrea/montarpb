// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.PlayerInfo
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class PlayerInfo
{
  [CompilerGenerated]
  [SpecialName]
  public uint get_LastXmasDate() => ((PlayerEvent) this).uint_3;

  [CompilerGenerated]
  [SpecialName]
  public void set_LastXmasDate(uint value) => ((PlayerEvent) this).uint_3 = value;

  [CompilerGenerated]
  [SpecialName]
  public uint get_LastQuestDate() => ((PlayerEvent) this).uint_4;

  [CompilerGenerated]
  [SpecialName]
  public void set_LastQuestDate(uint value) => ((PlayerEvent) this).uint_4 = value;

  public int Rank { get; set; }

  public int NickColor { get; set; }

  public long PlayerId { get; set; }

  public string Nickname { get; set; }

  public bool IsOnline { get; set; }

  public AccountStatus Status
  {
    get => this.accountStatus_0;
    [CompilerGenerated, SpecialName] set => ((PlayerInfo) this).accountStatus_0 = value;
  }
}
