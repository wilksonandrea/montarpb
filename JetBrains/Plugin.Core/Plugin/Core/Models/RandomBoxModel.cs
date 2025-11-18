// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.RandomBoxModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class RandomBoxModel
{
  [CompilerGenerated]
  [SpecialName]
  public int get_Percent() => ((RandomBoxItem) this).int_2;

  [CompilerGenerated]
  [SpecialName]
  public void set_Percent(int value) => ((RandomBoxItem) this).int_2 = value;

  [CompilerGenerated]
  [SpecialName]
  public bool get_Special() => ((RandomBoxItem) this).bool_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Special(bool value) => ((RandomBoxItem) this).bool_0 = value;

  public int ItemsCount { get; set; }

  public int MinPercent { get; set; }

  public int MaxPercent { get; set; }

  public List<RandomBoxItem> Items
  {
    get => this.list_0;
    [CompilerGenerated, SpecialName] set => ((RandomBoxModel) this).list_0 = value;
  }
}
