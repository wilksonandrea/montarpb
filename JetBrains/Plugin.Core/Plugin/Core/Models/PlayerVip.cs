// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.PlayerVip
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class PlayerVip
{
  public PlayerVip() => ((PlayerTitles) this).Slots = 1;

  public long Add(long value)
  {
    ((PlayerTitles) this).Flags = ((PlayerTitles) this).Flags | value;
    return ((PlayerTitles) this).Flags;
  }

  public bool Contains(long value) => (((PlayerTitles) this).Flags & value) == value || value == 0L;

  public void SetEquip(int value, [In] int obj1)
  {
    switch (value)
    {
      case 0:
        ((PlayerTitles) this).Equiped1 = obj1;
        break;
      case 1:
        ((PlayerTitles) this).Equiped2 = obj1;
        break;
      case 2:
        ((PlayerTitles) this).Equiped3 = obj1;
        break;
    }
  }

  public int GetEquip(int value)
  {
    switch (value)
    {
      case 0:
        return ((PlayerTitles) this).Equiped1;
      case 1:
        return ((PlayerTitles) this).Equiped2;
      case 2:
        return ((PlayerTitles) this).Equiped3;
      default:
        return 0;
    }
  }

  public long OwnerId { get; set; }

  public string Address { get; set; }

  public string Benefit
  {
    [CompilerGenerated, SpecialName] get => ((PlayerVip) this).string_1;
    [CompilerGenerated, SpecialName] set => ((PlayerVip) this).string_1 = value;
  }

  public uint Expirate
  {
    [CompilerGenerated, SpecialName] get => ((PlayerVip) this).uint_0;
    [CompilerGenerated, SpecialName] [param: In] set => ((PlayerVip) this).uint_0 = value;
  }
}
