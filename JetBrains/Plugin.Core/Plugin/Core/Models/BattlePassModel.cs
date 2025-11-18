// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.BattlePassModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class BattlePassModel
{
  [CompilerGenerated]
  [SpecialName]
  public string get_Message() => ((RHistoryModel) this).string_2;

  [CompilerGenerated]
  [SpecialName]
  public void set_Message(string value) => ((RHistoryModel) this).string_2 = value;

  [CompilerGenerated]
  [SpecialName]
  public ReportType get_Type() => ((RHistoryModel) this).reportType_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Type(ReportType value) => ((RHistoryModel) this).reportType_0 = value;

  public BattlePassModel()
  {
  }

  public int Id { get; set; }

  public int MaxDailyPoints { get; set; }

  public string Name { get; set; }

  public uint BeginDate { get; set; }

  public uint EndedDate { get; set; }

  public bool Enable { get; set; }

  public List<PassBoxModel> Cards { get; set; }

  public BattlePassModel() => this.Name = "";

  public bool SeasonIsEnabled()
  {
    uint num = uint.Parse(DateTime.Now.ToString("yyMMddHHmm"));
    return this.BeginDate <= num && num < this.EndedDate;
  }
}
