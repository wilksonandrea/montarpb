// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.MissionCardModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class MissionCardModel
{
  [CompilerGenerated]
  [SpecialName]
  public void set_Exp(int value) => ((MissionCardAwards) this).int_5 = value;

  [CompilerGenerated]
  [SpecialName]
  public int get_Gold() => ((MissionCardAwards) this).int_6;

  [CompilerGenerated]
  [SpecialName]
  public void set_Gold(int value) => ((MissionCardAwards) this).int_6 = value;

  public bool Unusable()
  {
    return ((MissionCardAwards) this).Ensign == 0 && ((MissionCardAwards) this).Medal == 0 && ((MissionCardAwards) this).Ribbon == 0 && ((MissionCardAwards) this).Exp == 0 && this.get_Gold() == 0;
  }

  public ClassType WeaponReq { get; set; }

  public MissionType MissionType { get; set; }

  public int MissionId { get; set; }

  public int MapId { get; set; }

  public int WeaponReqId { get; set; }

  public int MissionLimit { get; set; }

  public int MissionBasicId { get; set; }

  public int CardBasicId { get; set; }

  public int ArrayIdx
  {
    [CompilerGenerated, SpecialName] get => ((MissionCardModel) this).int_6;
    [CompilerGenerated, SpecialName] set => ((MissionCardModel) this).int_6 = value;
  }

  public int Flag
  {
    [CompilerGenerated, SpecialName] get => ((MissionCardModel) this).int_7;
    [CompilerGenerated, SpecialName] set => ((MissionCardModel) this).int_7 = value;
  }
}
