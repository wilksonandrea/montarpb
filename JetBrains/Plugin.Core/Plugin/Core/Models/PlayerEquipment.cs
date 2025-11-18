// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.PlayerEquipment
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.XML;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class PlayerEquipment
{
  [CompilerGenerated]
  [SpecialName]
  public void set_Level(int Character) => ((PlayerCompetitive) this).int_0 = Character;

  [CompilerGenerated]
  [SpecialName]
  public int get_Points() => ((PlayerCompetitive) this).int_1;

  [CompilerGenerated]
  [SpecialName]
  public void set_Points(int ItemId) => ((PlayerCompetitive) this).int_1 = ItemId;

  public CompetitiveRank Rank()
  {
    return CouponEffectXML.GetRank(((PlayerCompetitive) this).Level) ?? (CompetitiveRank) new EventLoginModel();
  }

  public long OwnerId { get; set; }

  public int WeaponPrimary { get; set; }

  public int WeaponSecondary { get; set; }

  public int WeaponMelee { get; set; }

  public int WeaponExplosive { get; set; }

  public int WeaponSpecial { get; set; }

  public int CharaRedId { get; set; }

  public int CharaBlueId { get; set; }

  public int PartHead { get; set; }

  public int PartFace { get; set; }

  public int PartJacket { get; set; }

  public int PartPocket { get; set; }

  public int PartGlove { get; set; }

  public int PartBelt { get; set; }

  public int PartHolster { get; set; }

  public int PartSkin { get; set; }

  public int BeretItem { get; set; }

  public int DinoItem { get; set; }

  public int AccessoryId { get; set; }

  public int SprayId
  {
    [CompilerGenerated, SpecialName] get => ((PlayerEquipment) this).int_18;
    [CompilerGenerated, SpecialName] set => ((PlayerEquipment) this).int_18 = value;
  }

  public int NameCardId
  {
    [CompilerGenerated, SpecialName] get => ((PlayerEquipment) this).int_19;
    [CompilerGenerated, SpecialName] set => ((PlayerEquipment) this).int_19 = value;
  }
}
