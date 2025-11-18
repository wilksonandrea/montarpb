// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.PlayerMissions
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class PlayerMissions
{
  [CompilerGenerated]
  [SpecialName]
  public int get_SprayId() => ((PlayerEquipment) this).int_18;

  [CompilerGenerated]
  [SpecialName]
  public void set_SprayId(int value) => ((PlayerEquipment) this).int_18 = value;

  [CompilerGenerated]
  [SpecialName]
  public int get_NameCardId() => ((PlayerEquipment) this).int_19;

  [CompilerGenerated]
  [SpecialName]
  public void set_NameCardId(int value) => ((PlayerEquipment) this).int_19 = value;

  public PlayerMissions()
  {
    ((PlayerEquipment) this).WeaponPrimary = 103004;
    ((PlayerEquipment) this).WeaponSecondary = 202003;
    ((PlayerEquipment) this).WeaponMelee = 301001;
    ((PlayerEquipment) this).WeaponExplosive = 407001;
    ((PlayerEquipment) this).WeaponSpecial = 508001;
    ((PlayerEquipment) this).CharaRedId = 601001;
    ((PlayerEquipment) this).CharaBlueId = 602002;
    ((PlayerEquipment) this).PartHead = 1000700000;
    ((PlayerEquipment) this).PartFace = 1000800000;
    ((PlayerEquipment) this).PartJacket = 1000900000;
    ((PlayerEquipment) this).PartPocket = 1001000000;
    ((PlayerEquipment) this).PartGlove = 1001100000;
    ((PlayerEquipment) this).PartBelt = 1001200000;
    ((PlayerEquipment) this).PartHolster = 1001300000;
    ((PlayerEquipment) this).PartSkin = 1001400000;
    ((PlayerEquipment) this).DinoItem = 1500511;
  }

  public long OwnerId { get; set; }

  public byte[] List1 { get; set; }

  public byte[] List2 { get; set; }

  public byte[] List3 { get; set; }

  public byte[] List4 { get; set; }

  public int ActualMission { get; set; }

  public int Card1 { get; set; }

  public int Card2 { get; set; }

  public int Card3 { get; set; }

  public int Card4 { get; set; }

  public int Mission1 { get; set; }

  public int Mission2 { get; set; }

  public int Mission3 { get; set; }

  public int Mission4 { get; set; }

  public bool SelectedCard { get; set; }

  public PlayerMissions()
  {
    this.List1 = new byte[40];
    this.List2 = new byte[40];
    this.List3 = new byte[40];
    this.List4 = new byte[40];
  }

  public PlayerMissions DeepCopy() => this.MemberwiseClone() as PlayerMissions;
}
