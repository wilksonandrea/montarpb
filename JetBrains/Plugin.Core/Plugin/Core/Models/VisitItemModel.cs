// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.VisitItemModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class VisitItemModel
{
  [CompilerGenerated]
  [SpecialName]
  public string get_Name() => ((TRuleModel) this).string_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Name(string value) => ((TRuleModel) this).string_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public List<int> get_BanIndexes() => ((TRuleModel) this).list_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_BanIndexes(List<int> value) => ((TRuleModel) this).list_0 = value;

  public int GoodId
  {
    get => this.int_0;
    [CompilerGenerated, SpecialName] set => ((VisitItemModel) this).int_0 = value;
  }

  public bool IsReward
  {
    [CompilerGenerated, SpecialName] get => ((VisitItemModel) this).bool_0;
    [CompilerGenerated, SpecialName] set => ((VisitItemModel) this).bool_0 = value;
  }
}
