// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.PassBoxModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class PassBoxModel
{
  [CompilerGenerated]
  [SpecialName]
  public long get_OwnerId() => ((NHistoryModel) this).long_1;

  [CompilerGenerated]
  [SpecialName]
  public void set_OwnerId(long value) => ((NHistoryModel) this).long_1 = value;

  [CompilerGenerated]
  [SpecialName]
  public uint get_ChangeDate() => ((NHistoryModel) this).uint_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_ChangeDate(uint value) => ((NHistoryModel) this).uint_0 = value;

  public PassItemModel Normal { get; set; }

  public PassItemModel PremiumA { get; set; }

  public PassItemModel PremiumB { get; set; }

  public int RequiredPoints { get; set; }

  public int RewardCount
  {
    get => this.int_1;
    [CompilerGenerated, SpecialName] set => ((PassBoxModel) this).int_1 = value;
  }

  public int Card
  {
    [CompilerGenerated, SpecialName] get => ((PassBoxModel) this).int_2;
    [CompilerGenerated, SpecialName] set => ((PassBoxModel) this).int_2 = value;
  }
}
