// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.BattleBoxModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class BattleBoxModel
{
  [CompilerGenerated]
  [SpecialName]
  public int get_GoodsId() => ((BattleBoxItem) this).int_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_GoodsId([In] int obj0) => ((BattleBoxItem) this).int_0 = obj0;

  [CompilerGenerated]
  [SpecialName]
  public int get_Percent() => ((BattleBoxItem) this).int_1;

  [CompilerGenerated]
  [SpecialName]
  public void set_Percent(int Text) => ((BattleBoxItem) this).int_1 = Text;

  public int CouponId { get; [param: In] set; }

  public int RequireTags { get; set; }

  public List<BattleBoxItem> Items { get; [param: In] set; }

  public List<int> Goods { get; set; }

  public List<double> Probabilities
  {
    [CompilerGenerated, SpecialName] get => ((BattleBoxModel) this).list_2;
    [CompilerGenerated, SpecialName] set => ((BattleBoxModel) this).list_2 = value;
  }
}
