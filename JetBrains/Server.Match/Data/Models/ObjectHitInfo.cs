// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Models.ObjectHitInfo
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core.Enums;
using Plugin.Core.SharpDX;
using Server.Match.Data.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Server.Match.Data.Models;

public class ObjectHitInfo
{
  [CompilerGenerated]
  [SpecialName]
  public void set_Objects(List<ObjectModel> value) => ((MapModel) this).list_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public List<BombPosition> get_Bombs() => ((MapModel) this).list_1;

  [CompilerGenerated]
  [SpecialName]
  public void set_Bombs(List<BombPosition> value) => ((MapModel) this).list_1 = value;

  public BombPosition GetBomb(int value)
  {
    try
    {
      return this.get_Bombs()[value];
    }
    catch
    {
      return (BombPosition) null;
    }
  }

  public int Type { get; set; }

  public int ObjSyncId { get; set; }

  public int ObjId { get; set; }

  public int ObjLife { get; set; }

  public int KillerSlot { get; set; }

  public int AnimId1 { get; set; }

  public int AnimId2 { get; set; }

  public int DestroyState { get; set; }

  public int WeaponId { get; set; }

  public byte Accessory { get; set; }

  public byte Extensions { get; set; }

  public float SpecialUse { get; set; }

  public Half3 Position { get; set; }

  public ClassType WeaponClass { get; set; }

  public CharaHitPart HitPart
  {
    [CompilerGenerated, SpecialName] get => ((ObjectHitInfo) this).charaHitPart_0;
    [CompilerGenerated, SpecialName] set => ((ObjectHitInfo) this).charaHitPart_0 = value;
  }

  public CharaDeath DeathType
  {
    [CompilerGenerated, SpecialName] get => ((ObjectHitInfo) this).charaDeath_0;
    [CompilerGenerated, SpecialName] set => ((ObjectHitInfo) this).charaDeath_0 = value;
  }
}
