// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.TitleModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class TitleModel
{
  [CompilerGenerated]
  [SpecialName]
  public int get_Id() => ((TitleAward) this).int_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Id(int value) => ((TitleAward) this).int_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public ItemsModel get_Item() => ((TitleAward) this).itemsModel_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Item(ItemsModel value) => ((TitleAward) this).itemsModel_0 = value;

  public int Id { get; set; }

  public int ClassId { get; set; }

  public int Medal { get; [param: In] set; }

  public int Ribbon { get; set; }

  public int MasterMedal { get; set; }

  public int Ensign { get; set; }

  public int Rank { get; set; }

  public int Slot { get; set; }

  public int Req1 { get; set; }

  public int Req2
  {
    get => this.int_9;
    [CompilerGenerated, SpecialName] set => ((TitleModel) this).int_9 = value;
  }

  public long Flag
  {
    [CompilerGenerated, SpecialName] get => ((TitleModel) this).long_0;
    [CompilerGenerated, SpecialName] set => ((TitleModel) this).long_0 = value;
  }
}
