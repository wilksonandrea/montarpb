// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.GoodsItem
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class GoodsItem
{
  [CompilerGenerated]
  [SpecialName]
  public int get_StageOptions() => ((QuickstartModel) this).int_2;

  [CompilerGenerated]
  [SpecialName]
  public void set_StageOptions(int value) => ((QuickstartModel) this).int_2 = value;

  [CompilerGenerated]
  [SpecialName]
  public int get_Type() => ((QuickstartModel) this).int_3;

  [CompilerGenerated]
  [SpecialName]
  public void set_Type(int string_1) => ((QuickstartModel) this).int_3 = string_1;

  public int Id { get; [param: In] set; }

  public int PriceGold { get; set; }

  public int PriceCash { get; set; }

  public int AuthType { get; set; }

  public int BuyType2 { get; set; }

  public int BuyType3 { get; set; }

  public int Title { get; set; }

  public int Visibility { get; set; }

  public int StarGold { get; set; }

  public int StarCash { get; set; }

  public ItemTag Tag
  {
    [CompilerGenerated, SpecialName] get => ((GoodsItem) this).itemTag_0;
    [CompilerGenerated, SpecialName] set => ((GoodsItem) this).itemTag_0 = value;
  }

  public ItemsModel Item
  {
    [CompilerGenerated, SpecialName] get => ((GoodsItem) this).itemsModel_0;
    [CompilerGenerated, SpecialName] set => ((GoodsItem) this).itemsModel_0 = value;
  }
}
