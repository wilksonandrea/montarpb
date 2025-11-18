// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.CartGoods
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class CartGoods
{
  [CompilerGenerated]
  [SpecialName]
  public bool get_Enable() => ((BattleRewardModel) this).bool_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Enable([In] bool obj0) => ((BattleRewardModel) this).bool_0 = obj0;

  [CompilerGenerated]
  [SpecialName]
  public int[] get_Rewards() => ((BattleRewardModel) this).int_1;

  [CompilerGenerated]
  [SpecialName]
  public void set_Rewards(int[] value) => ((BattleRewardModel) this).int_1 = value;

  public int GoodId
  {
    [CompilerGenerated, SpecialName] get => ((CartGoods) this).int_0;
    [CompilerGenerated, SpecialName] set => ((CartGoods) this).int_0 = value;
  }

  public int BuyType
  {
    [CompilerGenerated, SpecialName] get => ((CartGoods) this).int_1;
    [CompilerGenerated, SpecialName] set => ((CartGoods) this).int_1 = value;
  }
}
