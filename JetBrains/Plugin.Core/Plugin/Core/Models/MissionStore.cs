// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.MissionStore
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class MissionStore
{
  [CompilerGenerated]
  [SpecialName]
  public int get_MissionId() => ((MissionItemAward) this).int_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_MissionId(int value) => ((MissionItemAward) this).int_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public ItemsModel get_Item() => ((MissionItemAward) this).itemsModel_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Item(ItemsModel value) => ((MissionItemAward) this).itemsModel_0 = value;

  public int Id { get; set; }

  public int ItemId
  {
    [CompilerGenerated, SpecialName] get => ((MissionStore) this).int_1;
    [CompilerGenerated, SpecialName] set => ((MissionStore) this).int_1 = value;
  }

  public bool Enable
  {
    [CompilerGenerated, SpecialName] get => ((MissionStore) this).bool_0;
    [CompilerGenerated, SpecialName] [param: In] set => ((MissionStore) this).bool_0 = value;
  }
}
