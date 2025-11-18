// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.CompetitiveRank
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class CompetitiveRank
{
  [CompilerGenerated]
  [SpecialName]
  public int get_Minutes25() => ((CommandHelper) this).int_14;

  [CompilerGenerated]
  [SpecialName]
  public void set_Minutes25(int value) => ((CommandHelper) this).int_14 = value;

  [CompilerGenerated]
  [SpecialName]
  public int get_Minutes30() => ((CommandHelper) this).int_15;

  [CompilerGenerated]
  [SpecialName]
  public void set_Minutes30(int value) => ((CommandHelper) this).int_15 = value;

  public CompetitiveRank(string value) => ((CommandHelper) this).Tag = value;

  public int Id { get; set; }

  public int TourneyLevel { get; set; }

  public int Points
  {
    [CompilerGenerated, SpecialName] get => ((CompetitiveRank) this).int_2;
    [CompilerGenerated, SpecialName] set => ((CompetitiveRank) this).int_2 = value;
  }

  public string Name
  {
    [CompilerGenerated, SpecialName] get => ((CompetitiveRank) this).string_0;
    [CompilerGenerated, SpecialName] set => ((CompetitiveRank) this).string_0 = value;
  }
}
