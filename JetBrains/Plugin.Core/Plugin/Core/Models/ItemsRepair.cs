// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.ItemsRepair
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class ItemsRepair
{
  [CompilerGenerated]
  [SpecialName]
  public ItemTag get_Tag() => ((GoodsItem) this).itemTag_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Tag(ItemTag value) => ((GoodsItem) this).itemTag_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public ItemsModel get_Item() => ((GoodsItem) this).itemsModel_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Item(ItemsModel value) => ((GoodsItem) this).itemsModel_0 = value;

  public ItemsRepair()
  {
    PlayerBonus playerBonus = new PlayerBonus();
    ((ItemsModel) playerBonus).Equip = ItemEquipType.Durable;
    this.set_Item((ItemsModel) playerBonus);
  }

  public int Id { get; set; }

  public int Point { get; set; }

  public int Cash { get; set; }

  public uint Quantity
  {
    [CompilerGenerated, SpecialName] get => ((ItemsRepair) this).uint_0;
    [CompilerGenerated, SpecialName] set => ((ItemsRepair) this).uint_0 = value;
  }

  public bool Enable
  {
    [CompilerGenerated, SpecialName] get => ((ItemsRepair) this).bool_0;
    [CompilerGenerated, SpecialName] set => ((ItemsRepair) this).bool_0 = value;
  }
}
