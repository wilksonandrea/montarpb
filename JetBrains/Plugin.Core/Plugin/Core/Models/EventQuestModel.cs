// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.EventQuestModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Utility;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class EventQuestModel
{
  [CompilerGenerated]
  [SpecialName]
  public void set_Goods2(List<int> value) => ((EventPlaytimeModel) this).list_1 = value;

  [CompilerGenerated]
  [SpecialName]
  public List<int> get_Goods3() => ((EventPlaytimeModel) this).list_2;

  [CompilerGenerated]
  [SpecialName]
  public void set_Goods3(List<int> value) => ((EventPlaytimeModel) this).list_2 = value;

  public EventQuestModel()
  {
    ((EventPlaytimeModel) this).Name = "";
    ((EventPlaytimeModel) this).Description = "";
  }

  public bool EventIsEnabled()
  {
    uint num = uint.Parse(DBQuery.Now("yyMMddHHmm"));
    return ((EventPlaytimeModel) this).BeginDate <= num && num < ((EventPlaytimeModel) this).EndedDate;
  }

  public uint BeginDate
  {
    [CompilerGenerated, SpecialName] get => ((EventQuestModel) this).uint_0;
    [CompilerGenerated, SpecialName] set => ((EventQuestModel) this).uint_0 = value;
  }

  public uint EndedDate
  {
    [CompilerGenerated, SpecialName] get => ((EventQuestModel) this).uint_1;
    [CompilerGenerated, SpecialName] set => ((EventQuestModel) this).uint_1 = value;
  }
}
