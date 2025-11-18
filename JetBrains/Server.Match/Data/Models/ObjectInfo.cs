// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Models.ObjectInfo
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core.Enums;
using Server.Match.Data.Enums;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Server.Match.Data.Models;

public class ObjectInfo
{
  [CompilerGenerated]
  [SpecialName]
  public CharaHitPart get_HitPart() => ((ObjectHitInfo) this).charaHitPart_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_HitPart(CharaHitPart value) => ((ObjectHitInfo) this).charaHitPart_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public CharaDeath get_DeathType() => ((ObjectHitInfo) this).charaDeath_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_DeathType(CharaDeath value) => ((ObjectHitInfo) this).charaDeath_0 = value;

  public ObjectInfo(int value)
  {
    ((ObjectHitInfo) this).Type = value;
    this.set_DeathType(CharaDeath.DEFAULT);
  }

  public int Id { get; set; }

  public int Life { get; set; }

  public int DestroyState { get; set; }

  public AnimModel Animation { get; set; }

  public DateTime UseDate
  {
    [CompilerGenerated, SpecialName] get => ((ObjectInfo) this).dateTime_0;
    [CompilerGenerated, SpecialName] set => ((ObjectInfo) this).dateTime_0 = value;
  }

  public ObjectModel Model
  {
    [CompilerGenerated, SpecialName] get => ((ObjectInfo) this).objectModel_0;
    [CompilerGenerated, SpecialName] set => ((ObjectInfo) this).objectModel_0 = value;
  }
}
