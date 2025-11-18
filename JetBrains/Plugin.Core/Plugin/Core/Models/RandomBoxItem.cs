// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.RandomBoxItem
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class RandomBoxItem
{
  [CompilerGenerated]
  [SpecialName]
  public int get_Tag() => ((MapMatch) this).int_3;

  [CompilerGenerated]
  [SpecialName]
  public void set_Tag(int value) => ((MapMatch) this).int_3 = value;

  [CompilerGenerated]
  [SpecialName]
  public string get_Name() => ((MapMatch) this).string_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Name(string int_5) => ((MapMatch) this).string_0 = int_5;

  public RandomBoxItem(int value) => ((MapMatch) this).Mode = value;

  public int Index { get; set; }

  public int GoodsId { get; set; }

  public int Percent
  {
    [CompilerGenerated, SpecialName] get => ((RandomBoxItem) this).int_2;
    [CompilerGenerated, SpecialName] set => ((RandomBoxItem) this).int_2 = value;
  }

  public bool Special
  {
    [CompilerGenerated, SpecialName] get => ((RandomBoxItem) this).bool_0;
    [CompilerGenerated, SpecialName] set => ((RandomBoxItem) this).bool_0 = value;
  }
}
