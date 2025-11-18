// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.EventLoginModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class EventLoginModel
{
  [CompilerGenerated]
  [SpecialName]
  public int get_Points() => ((CompetitiveRank) this).int_2;

  [CompilerGenerated]
  [SpecialName]
  public void set_Points(int value) => ((CompetitiveRank) this).int_2 = value;

  [CompilerGenerated]
  [SpecialName]
  public string get_Name() => ((CompetitiveRank) this).string_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Name(string value) => ((CompetitiveRank) this).string_0 = value;

  public int Id { get; set; }

  public uint BeginDate { get; set; }

  public uint EndedDate { get; set; }

  public string Name { get; set; }

  public string Description { get; set; }

  public bool Period { get; set; }

  public bool Priority
  {
    get => this.bool_1;
    [CompilerGenerated, SpecialName] set => ((EventLoginModel) this).bool_1 = value;
  }

  public List<int> Goods
  {
    [CompilerGenerated, SpecialName] get => ((EventLoginModel) this).list_0;
    [CompilerGenerated, SpecialName] set => ((EventLoginModel) this).list_0 = value;
  }
}
