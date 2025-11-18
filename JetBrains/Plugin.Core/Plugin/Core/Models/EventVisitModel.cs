// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.EventVisitModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Utility;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class EventVisitModel
{
  [CompilerGenerated]
  [SpecialName]
  public List<int[]> get_Ranks() => ((EventRankUpModel) this).list_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Ranks(List<int[]> value) => ((EventRankUpModel) this).list_0 = value;

  public EventVisitModel()
  {
    ((EventRankUpModel) this).Name = "";
    ((EventRankUpModel) this).Description = "";
  }

  public bool EventIsEnabled()
  {
    uint num = uint.Parse(DBQuery.Now("yyMMddHHmm"));
    return ((EventRankUpModel) this).BeginDate <= num && num < ((EventRankUpModel) this).EndedDate;
  }

  public int[] GetBonuses(int value)
  {
    lock (this.get_Ranks())
    {
      foreach (int[] numArray in this.get_Ranks())
      {
        if (numArray[0] == value)
          return new int[3]
          {
            numArray[1],
            numArray[2],
            numArray[3]
          };
      }
      return new int[3];
    }
  }

  public int Id { get; set; }

  public uint BeginDate { get; set; }

  public uint EndedDate { get; set; }

  public int Checks { get; set; }

  public string Title { get; set; }

  public List<VisitBoxModel> Boxes
  {
    get => this.list_0;
    [CompilerGenerated, SpecialName] set => ((EventVisitModel) this).list_0 = value;
  }
}
