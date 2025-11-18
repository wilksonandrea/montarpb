// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.MapMatch
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class MapMatch
{
  [CompilerGenerated]
  [SpecialName]
  public int get_PremiumExp() => ((InternetCafe) this).int_3;

  [CompilerGenerated]
  [SpecialName]
  public void set_PremiumExp(int value) => ((InternetCafe) this).int_3 = value;

  [CompilerGenerated]
  [SpecialName]
  public int get_PremiumGold() => ((InternetCafe) this).int_4;

  [CompilerGenerated]
  [SpecialName]
  public void set_PremiumGold(int value) => ((InternetCafe) this).int_4 = value;

  public MapMatch(int value) => ((InternetCafe) this).ConfigId = value;

  public int Mode { get; set; }

  public int Id { get; set; }

  public int Limit { get; set; }

  public int Tag
  {
    [CompilerGenerated, SpecialName] get => ((MapMatch) this).int_3;
    [CompilerGenerated, SpecialName] set => ((MapMatch) this).int_3 = value;
  }

  public string Name
  {
    [CompilerGenerated, SpecialName] get => ((MapMatch) this).string_0;
    [CompilerGenerated, SpecialName] set => ((MapMatch) this).string_0 = value;
  }
}
