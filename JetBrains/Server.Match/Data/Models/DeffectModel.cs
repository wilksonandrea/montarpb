// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Models.DeffectModel
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using System.Runtime.CompilerServices;

#nullable disable
namespace Server.Match.Data.Models;

public class DeffectModel
{
  [CompilerGenerated]
  [SpecialName]
  public PlayerModel get_Player() => ((DeathServerData) this).playerModel_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Player(PlayerModel value) => ((DeathServerData) this).playerModel_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public int get_AssistSlot() => ((DeathServerData) this).int_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_AssistSlot(int value) => ((DeathServerData) this).int_0 = value;

  public DeffectModel() => this.set_AssistSlot(-1);

  public int Id
  {
    [CompilerGenerated, SpecialName] get => ((DeffectModel) this).int_0;
    [CompilerGenerated, SpecialName] set => ((DeffectModel) this).int_0 = value;
  }

  public int Life
  {
    [CompilerGenerated, SpecialName] get => ((DeffectModel) this).int_1;
    [CompilerGenerated, SpecialName] set => ((DeffectModel) this).int_1 = value;
  }
}
