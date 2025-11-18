// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.BattleRewardItem
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class BattleRewardItem
{
  [CompilerGenerated]
  [SpecialName]
  public List<double> get_Probabilities() => ((BattleBoxModel) this).list_2;

  [CompilerGenerated]
  [SpecialName]
  public void set_Probabilities(List<double> value) => ((BattleBoxModel) this).list_2 = value;

  public T GetItemWithProbabilities<T>(List<T> value, [In] List<double> obj1)
  {
    if (value == null || value.Count == 0 || obj1 == null || obj1.Count == 0 || value.Count != obj1.Count)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("Battle Box Item List Is Not Valid!", LoggerType.Warning, (Exception) null);
    }
    double num1 = new Random().NextDouble();
    double num2 = 0.0;
    for (int index = 0; index < value.Count; ++index)
    {
      num2 += obj1[index] / 100.0;
      if (num1 < num2)
        return value[index];
    }
    return value[value.Count - 1];
  }

  public void InitItemPercentages()
  {
    ((BattleBoxModel) this).Goods = new List<int>();
    this.set_Probabilities(new List<double>());
    foreach (BattleBoxItem battleBoxItem in ((BattleBoxModel) this).Items)
    {
      ((BattleBoxModel) this).Goods.Add(((BattleBoxModel) battleBoxItem).get_GoodsId());
      this.get_Probabilities().Add((double) ((BattleBoxModel) battleBoxItem).get_Percent());
    }
  }

  public int Index
  {
    [CompilerGenerated, SpecialName] get => ((BattleRewardItem) this).int_0;
    [CompilerGenerated, SpecialName] set => ((BattleRewardItem) this).int_0 = value;
  }

  public int GoodId
  {
    [CompilerGenerated, SpecialName] get => ((BattleRewardItem) this).int_1;
    [CompilerGenerated, SpecialName] set => ((BattleRewardItem) this).int_1 = value;
  }
}
