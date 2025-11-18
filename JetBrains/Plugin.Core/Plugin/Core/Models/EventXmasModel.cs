// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.EventXmasModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Utility;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class EventXmasModel
{
  [CompilerGenerated]
  [SpecialName]
  public void set_Boxes(List<VisitBoxModel> value) => ((EventVisitModel) this).list_0 = value;

  public EventXmasModel()
  {
    ((EventVisitModel) this).Checks = 31 /*0x1F*/;
    ((EventVisitModel) this).Title = "";
  }

  public bool EventIsEnabled()
  {
    uint num = uint.Parse(DBQuery.Now("yyMMddHHmm"));
    return ((EventVisitModel) this).BeginDate <= num && num < ((EventVisitModel) this).EndedDate;
  }

  public List<VisitItemModel> GetReward(int value, [In] int obj1)
  {
    List<VisitItemModel> reward = new List<VisitItemModel>();
    switch (obj1)
    {
      case 0:
        reward.Add(((EventVisitModel) this).Boxes[value].Reward1);
        break;
      case 1:
        reward.Add(((EventVisitModel) this).Boxes[value].Reward2);
        break;
      default:
        reward.Add(((EventVisitModel) this).Boxes[value].Reward1);
        reward.Add(((EventVisitModel) this).Boxes[value].Reward2);
        break;
    }
    return reward;
  }

  public void SetBoxCounts()
  {
    for (int index = 0; index < 31 /*0x1F*/; ++index)
      ((TicketModel) ((EventVisitModel) this).Boxes[index]).SetCount();
  }

  public uint BeginDate { get; set; }

  public uint EndedDate
  {
    [CompilerGenerated, SpecialName] get => ((EventXmasModel) this).uint_1;
    [CompilerGenerated, SpecialName] set => ((EventXmasModel) this).uint_1 = value;
  }

  public int GoodId
  {
    [CompilerGenerated, SpecialName] get => ((EventXmasModel) this).int_0;
    [CompilerGenerated, SpecialName] set => ((EventXmasModel) this).int_0 = value;
  }
}
