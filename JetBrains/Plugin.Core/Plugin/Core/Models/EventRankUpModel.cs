// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.EventRankUpModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class EventRankUpModel
{
  [CompilerGenerated]
  [SpecialName]
  public uint get_BeginDate() => ((EventQuestModel) this).uint_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_BeginDate(uint value) => ((EventQuestModel) this).uint_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public uint get_EndedDate() => ((EventQuestModel) this).uint_1;

  [CompilerGenerated]
  [SpecialName]
  public void set_EndedDate(uint value) => ((EventQuestModel) this).uint_1 = value;

  public int Id { get; set; }

  public uint BeginDate { get; set; }

  public uint EndedDate { get; set; }

  public string Name { get; set; }

  public string Description { get; set; }

  public bool Period { get; set; }

  public bool Priority { get; set; }

  public List<int[]> Ranks
  {
    [CompilerGenerated, SpecialName] get => ((EventRankUpModel) this).list_0;
    [CompilerGenerated, SpecialName] set => ((EventRankUpModel) this).list_0 = value;
  }
}
