// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.Map.MapRule
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models.Map;

public class MapRule
{
  [CompilerGenerated]
  [SpecialName]
  public uint get_Quantity() => ((ItemsRepair) this).uint_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Quantity(uint value) => ((ItemsRepair) this).uint_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public bool get_Enable() => ((ItemsRepair) this).bool_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Enable(bool value) => ((ItemsRepair) this).bool_0 = value;

  public string Name { get; set; }

  public int Id { get; set; }

  public int Rule { get; set; }

  public int StageOptions
  {
    [CompilerGenerated, SpecialName] get => ((MapRule) this).int_2;
    [CompilerGenerated, SpecialName] set => ((MapRule) this).int_2 = value;
  }

  public int Conditions
  {
    [CompilerGenerated, SpecialName] get => ((MapRule) this).int_3;
    [CompilerGenerated, SpecialName] set => ((MapRule) this).int_3 = value;
  }
}
