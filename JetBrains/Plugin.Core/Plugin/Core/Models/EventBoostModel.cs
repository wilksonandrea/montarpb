// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.EventBoostModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.Utility;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class EventBoostModel
{
  [CompilerGenerated]
  [SpecialName]
  public void set_Priority(bool value) => ((EventLoginModel) this).bool_1 = value;

  [CompilerGenerated]
  [SpecialName]
  public List<int> get_Goods() => ((EventLoginModel) this).list_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Goods(List<int> value) => ((EventLoginModel) this).list_0 = value;

  public EventBoostModel()
  {
    ((EventLoginModel) this).Name = "";
    ((EventLoginModel) this).Description = "";
  }

  public bool EventIsEnabled()
  {
    uint num = uint.Parse(DBQuery.Now("yyMMddHHmm"));
    return ((EventLoginModel) this).BeginDate <= num && num < ((EventLoginModel) this).EndedDate;
  }

  public int Id { get; set; }

  public int BonusExp { get; set; }

  public int BonusGold { get; set; }

  public int Percent { get; set; }

  public uint BeginDate { get; set; }

  public uint EndedDate { get; set; }

  public string Name { get; set; }

  public string Description { get; set; }

  public bool Period { get; set; }

  public bool Priority { get; set; }

  public PortalBoostEvent BoostType
  {
    get => this.portalBoostEvent_0;
    [CompilerGenerated, SpecialName] set => ((EventBoostModel) this).portalBoostEvent_0 = value;
  }

  public int BoostValue
  {
    [CompilerGenerated, SpecialName] get => ((EventBoostModel) this).int_4;
    [CompilerGenerated, SpecialName] set => ((EventBoostModel) this).int_4 = value;
  }
}
