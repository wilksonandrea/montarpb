// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.SlotChange
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class SlotChange
{
  [CompilerGenerated]
  [SpecialName]
  public int get_ItemsCount() => ((ShopData) this).int_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_ItemsCount(int value) => ((ShopData) this).int_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public int get_Offset() => ((ShopData) this).int_1;

  [CompilerGenerated]
  [SpecialName]
  public void set_Offset(int value) => ((ShopData) this).int_1 = value;

  public SlotModel OldSlot
  {
    [CompilerGenerated, SpecialName] get => ((SlotChange) this).slotModel_0;
    [CompilerGenerated, SpecialName] set => ((SlotChange) this).slotModel_0 = value;
  }

  public SlotModel NewSlot
  {
    [CompilerGenerated, SpecialName] get => ((SlotChange) this).slotModel_1;
    [CompilerGenerated, SpecialName] set => ((SlotChange) this).slotModel_1 = value;
  }
}
