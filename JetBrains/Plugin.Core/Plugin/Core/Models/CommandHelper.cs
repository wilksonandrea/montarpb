// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.CommandHelper
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class CommandHelper
{
  [CompilerGenerated]
  [SpecialName]
  public int get_GoodId() => ((CartGoods) this).int_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_GoodId(int value) => ((CartGoods) this).int_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public int get_BuyType() => ((CartGoods) this).int_1;

  [CompilerGenerated]
  [SpecialName]
  public void set_BuyType(int value) => ((CartGoods) this).int_1 = value;

  public string Tag { get; set; }

  public int AllWeapons { get; set; }

  public int AssaultRifle { get; set; }

  public int SubMachineGun { get; set; }

  public int SniperRifle { get; set; }

  public int ShotGun { get; set; }

  public int MachineGun { get; set; }

  public int Secondary { get; set; }

  public int Melee { get; set; }

  public int Knuckle { get; set; }

  public int RPG7 { get; set; }

  public int Minutes05 { get; set; }

  public int Minutes10 { get; set; }

  public int Minutes15 { get; set; }

  public int Minutes20 { get; set; }

  public int Minutes25
  {
    [CompilerGenerated, SpecialName] get => ((CommandHelper) this).int_14;
    [CompilerGenerated, SpecialName] set => ((CommandHelper) this).int_14 = value;
  }

  public int Minutes30
  {
    [CompilerGenerated, SpecialName] get => ((CommandHelper) this).int_15;
    [CompilerGenerated, SpecialName] set => ((CommandHelper) this).int_15 = value;
  }
}
