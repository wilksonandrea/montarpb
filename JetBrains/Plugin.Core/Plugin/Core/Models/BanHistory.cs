// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.BanHistory
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class BanHistory
{
  [CompilerGenerated]
  [SpecialName]
  public void set_GoodId(int value) => ((VisitItemModel) this).int_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public bool get_IsReward() => ((VisitItemModel) this).bool_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_IsReward(bool value) => ((VisitItemModel) this).bool_0 = value;

  public void SetGoodId(int int_10)
  {
    this.set_GoodId(int_10);
    if (int_10 <= 0)
      return;
    this.set_IsReward(true);
  }

  public long ObjectId { get; set; }

  public long PlayerId { get; set; }

  public string Type { get; set; }

  public string Value { get; set; }

  public string Reason { get; set; }

  public DateTime StartDate
  {
    [CompilerGenerated, SpecialName] get => ((BanHistory) this).dateTime_0;
    [CompilerGenerated, SpecialName] set => ((BanHistory) this).dateTime_0 = value;
  }

  public DateTime EndDate
  {
    [CompilerGenerated, SpecialName] get => ((BanHistory) this).dateTime_1;
    [CompilerGenerated, SpecialName] set => ((BanHistory) this).dateTime_1 = value;
  }
}
