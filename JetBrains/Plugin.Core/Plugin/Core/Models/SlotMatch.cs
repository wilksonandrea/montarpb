// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.SlotMatch
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class SlotMatch
{
  [CompilerGenerated]
  [SpecialName]
  public SlotModel get_OldSlot() => ((SlotChange) this).slotModel_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_OldSlot(SlotModel value) => ((SlotChange) this).slotModel_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public SlotModel get_NewSlot() => ((SlotChange) this).slotModel_1;

  [CompilerGenerated]
  [SpecialName]
  public void set_NewSlot(SlotModel value) => ((SlotChange) this).slotModel_1 = value;

  public SlotMatch(SlotModel value, [In] SlotModel obj1)
  {
    this.set_OldSlot(value);
    this.set_NewSlot(obj1);
  }

  public int Id { get; set; }

  public long PlayerId
  {
    [CompilerGenerated, SpecialName] get => ((SlotMatch) this).long_0;
    [CompilerGenerated, SpecialName] set => ((SlotMatch) this).long_0 = value;
  }

  public SlotMatchState State
  {
    [CompilerGenerated, SpecialName] get => ((SlotMatch) this).slotMatchState_0;
    [CompilerGenerated, SpecialName] set => ((SlotMatch) this).slotMatchState_0 = value;
  }
}
