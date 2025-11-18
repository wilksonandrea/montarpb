// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.Synchronize
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Net;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class Synchronize
{
  [CompilerGenerated]
  [SpecialName]
  public int get_ShieldKills() => ((StatisticWeapon) this).int_10;

  [CompilerGenerated]
  [SpecialName]
  public void set_ShieldKills(int value) => ((StatisticWeapon) this).int_10 = value;

  [CompilerGenerated]
  [SpecialName]
  public int get_ShieldDeaths() => ((StatisticWeapon) this).int_11;

  [CompilerGenerated]
  [SpecialName]
  public void set_ShieldDeaths(int value) => ((StatisticWeapon) this).int_11 = value;

  public int RemotePort
  {
    [CompilerGenerated, SpecialName] get => ((Synchronize) this).int_0;
    [CompilerGenerated, SpecialName] set => ((Synchronize) this).int_0 = value;
  }

  public IPEndPoint Connection
  {
    [CompilerGenerated, SpecialName] get => ((Synchronize) this).ipendPoint_0;
    [CompilerGenerated, SpecialName] set => ((Synchronize) this).ipendPoint_0 = value;
  }
}
