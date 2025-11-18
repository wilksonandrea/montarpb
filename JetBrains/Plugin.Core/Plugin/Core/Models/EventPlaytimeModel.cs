// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.EventPlaytimeModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.Utility;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class EventPlaytimeModel
{
  [CompilerGenerated]
  [SpecialName]
  public void set_BoostType(PortalBoostEvent value)
  {
    // ISSUE: reference to a compiler-generated field
    ((EventBoostModel) this).portalBoostEvent_0 = value;
  }

  [CompilerGenerated]
  [SpecialName]
  public int get_BoostValue() => ((EventBoostModel) this).int_4;

  [CompilerGenerated]
  [SpecialName]
  public void set_BoostValue(int value) => ((EventBoostModel) this).int_4 = value;

  public EventPlaytimeModel()
  {
    ((EventBoostModel) this).Name = "";
    ((EventBoostModel) this).Description = "";
  }

  public bool EventIsEnabled()
  {
    uint num = uint.Parse(DBQuery.Now("yyMMddHHmm"));
    return ((EventBoostModel) this).BeginDate <= num && num < ((EventBoostModel) this).EndedDate;
  }

  public int Id { get; set; }

  public uint BeginDate { get; set; }

  public uint EndedDate { get; set; }

  public string Name { get; set; }

  public string Description { get; set; }

  public int Minutes1 { get; set; }

  public int Minutes2 { get; set; }

  public int Minutes3 { get; set; }

  public bool Period { get; set; }

  public bool Priority { get; set; }

  public List<int> Goods1 { get; set; }

  public List<int> Goods2
  {
    get => this.list_1;
    [CompilerGenerated, SpecialName] set => ((EventPlaytimeModel) this).list_1 = value;
  }

  public List<int> Goods3
  {
    [CompilerGenerated, SpecialName] get => ((EventPlaytimeModel) this).list_2;
    [CompilerGenerated, SpecialName] set => ((EventPlaytimeModel) this).list_2 = value;
  }
}
