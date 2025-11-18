// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Models.DeathServerData
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core.Enums;
using System.Runtime.CompilerServices;

#nullable disable
namespace Server.Match.Data.Models;

public class DeathServerData
{
  [CompilerGenerated]
  [SpecialName]
  public int get_Id() => ((CharaModel) this).int_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Id(int value) => ((CharaModel) this).int_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public int get_HP() => ((CharaModel) this).int_1;

  [CompilerGenerated]
  [SpecialName]
  public void set_HP(int value) => ((CharaModel) this).int_1 = value;

  public CharaDeath DeathType { get; set; }

  public PlayerModel Player
  {
    [CompilerGenerated, SpecialName] get => ((DeathServerData) this).playerModel_0;
    [CompilerGenerated, SpecialName] set => ((DeathServerData) this).playerModel_0 = value;
  }

  public int AssistSlot
  {
    [CompilerGenerated, SpecialName] get => ((DeathServerData) this).int_0;
    [CompilerGenerated, SpecialName] set => ((DeathServerData) this).int_0 = value;
  }
}
