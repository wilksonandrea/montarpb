// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.TRuleModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class TRuleModel
{
  [CompilerGenerated]
  [SpecialName]
  public void set_Req2(int value) => ((TitleModel) this).int_9 = value;

  [CompilerGenerated]
  [SpecialName]
  public long get_Flag() => ((TitleModel) this).long_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Flag(long value) => ((TitleModel) this).long_0 = value;

  public TRuleModel()
  {
  }

  public TRuleModel(int value)
  {
    ((TitleModel) this).Id = value;
    this.set_Flag(1L << value);
  }

  public string Name
  {
    [CompilerGenerated, SpecialName] get => ((TRuleModel) this).string_0;
    [CompilerGenerated, SpecialName] set => ((TRuleModel) this).string_0 = value;
  }

  public List<int> BanIndexes
  {
    [CompilerGenerated, SpecialName] get => ((TRuleModel) this).list_0;
    [CompilerGenerated, SpecialName] set => ((TRuleModel) this).list_0 = value;
  }
}
