// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.MissionCardAwards
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class MissionCardAwards
{
  [CompilerGenerated]
  [SpecialName]
  public int get_Exp() => ((MissionAwards) this).int_2;

  [CompilerGenerated]
  [SpecialName]
  public void set_Exp(int value) => ((MissionAwards) this).int_2 = value;

  [CompilerGenerated]
  [SpecialName]
  public int get_Gold() => ((MissionAwards) this).int_3;

  [CompilerGenerated]
  [SpecialName]
  public void set_Gold(int value) => ((MissionAwards) this).int_3 = value;

  public MissionCardAwards(int value, [In] int obj1, [In] int obj2, [In] int obj3)
  {
    ((MissionAwards) this).Id = value;
    ((MissionAwards) this).MasterMedal = obj1;
    this.set_Exp(obj2);
    this.set_Gold(obj3);
  }

  public int Id { get; set; }

  public int Card { get; set; }

  public int Ensign { get; [param: In] set; }

  public int Medal { get; [param: In] set; }

  public int Ribbon { get; [param: In] set; }

  public int Exp
  {
    get => this.int_5;
    [CompilerGenerated, SpecialName] set => ((MissionCardAwards) this).int_5 = value;
  }

  public int Gold
  {
    [CompilerGenerated, SpecialName] get => ((MissionCardAwards) this).int_6;
    [CompilerGenerated, SpecialName] set => ((MissionCardAwards) this).int_6 = value;
  }
}
