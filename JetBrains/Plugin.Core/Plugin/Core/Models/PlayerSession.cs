// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.PlayerSession
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class PlayerSession
{
  [CompilerGenerated]
  [SpecialName]
  public int get_PlayersFR() => ((ClanTeam) this).int_1;

  [CompilerGenerated]
  [SpecialName]
  public void set_PlayersFR(int value) => ((ClanTeam) this).int_1 = value;

  [CompilerGenerated]
  [SpecialName]
  public int get_PlayersCT() => ((ClanTeam) this).int_2;

  [CompilerGenerated]
  [SpecialName]
  public void set_PlayersCT(int value) => ((ClanTeam) this).int_2 = value;

  public int SessionId
  {
    [CompilerGenerated, SpecialName] get => ((PlayerSession) this).int_0;
    [CompilerGenerated, SpecialName] set => ((PlayerSession) this).int_0 = value;
  }

  public long PlayerId
  {
    [CompilerGenerated, SpecialName] get => ((PlayerSession) this).long_0;
    [CompilerGenerated, SpecialName] set => ((PlayerSession) this).long_0 = value;
  }
}
