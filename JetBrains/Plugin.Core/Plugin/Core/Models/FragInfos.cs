// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.FragInfos
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class FragInfos
{
  [CompilerGenerated]
  [SpecialName]
  public byte get_AssistSlot() => ((FragModel) this).byte_4;

  [CompilerGenerated]
  [SpecialName]
  public void set_AssistSlot(byte value) => ((FragModel) this).byte_4 = value;

  [CompilerGenerated]
  [SpecialName]
  public byte[] get_Unks() => ((FragModel) this).byte_5;

  [CompilerGenerated]
  [SpecialName]
  public void set_Unks(byte[] value) => ((FragModel) this).byte_5 = value;

  public byte KillerSlot { get; set; }

  public byte KillsCount { get; set; }

  public byte Flag { get; set; }

  public byte Unk { get; set; }

  public CharaKillType KillingType { get; set; }

  public int WeaponId { get; set; }

  public int Score { get; set; }

  public float X { get; set; }

  public float Y { get; set; }

  public float Z
  {
    get => this.float_2;
    [CompilerGenerated, SpecialName] set => ((FragInfos) this).float_2 = value;
  }

  public List<FragModel> Frags
  {
    [CompilerGenerated, SpecialName] get => ((FragInfos) this).list_0;
    [CompilerGenerated, SpecialName] set => ((FragInfos) this).list_0 = value;
  }
}
