// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.BattleRewardModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class BattleRewardModel
{
  [CompilerGenerated]
  [SpecialName]
  public int get_Index() => ((BattleRewardItem) this).int_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Index(int value) => ((BattleRewardItem) this).int_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public int get_GoodId() => ((BattleRewardItem) this).int_1;

  [CompilerGenerated]
  [SpecialName]
  public void set_GoodId(int value) => ((BattleRewardItem) this).int_1 = value;

  public BattleRewardType Type { get; set; }

  public int Percentage { get; set; }

  public bool Enable
  {
    [CompilerGenerated, SpecialName] get => ((BattleRewardModel) this).bool_0;
    [CompilerGenerated, SpecialName] [param: In] set => ((BattleRewardModel) this).bool_0 = value;
  }

  public int[] Rewards
  {
    [CompilerGenerated, SpecialName] get => ((BattleRewardModel) this).int_1;
    [CompilerGenerated, SpecialName] set => ((BattleRewardModel) this).int_1 = value;
  }
}
