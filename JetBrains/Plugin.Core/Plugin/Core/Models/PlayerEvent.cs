// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.PlayerEvent
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class PlayerEvent
{
  [CompilerGenerated]
  [SpecialName]
  public string get_Macro5() => ((PlayerConfig) this).string_4;

  [CompilerGenerated]
  [SpecialName]
  public void set_Macro5(string value) => ((PlayerConfig) this).string_4 = value;

  [CompilerGenerated]
  [SpecialName]
  public byte[] get_KeyboardKeys() => ((PlayerConfig) this).byte_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_KeyboardKeys(byte[] value) => ((PlayerConfig) this).byte_0 = value;

  public PlayerEvent()
  {
    ((PlayerConfig) this).AudioSFX = 100;
    ((PlayerConfig) this).AudioBGM = 60;
    ((PlayerConfig) this).Crosshair = 2;
    ((PlayerConfig) this).Sensitivity = 50;
    ((PlayerConfig) this).PointOfView = 80 /*0x50*/;
    ((PlayerConfig) this).ShowBlood = 11;
    ((PlayerConfig) this).AudioEnable = 7;
    ((PlayerConfig) this).Config = 55;
    ((PlayerConfig) this).Macro = 31 /*0x1F*/;
    ((PlayerConfig) this).Macro1 = "";
    ((PlayerConfig) this).Macro2 = "";
    ((PlayerConfig) this).Macro3 = "";
    ((PlayerConfig) this).Macro4 = "";
    this.set_Macro5("");
    ((PlayerConfig) this).Nations = 0;
    this.set_KeyboardKeys(new byte[240 /*0xF0*/]);
  }

  public long OwnerId { get; set; }

  public int LastQuestFinish { get; set; }

  public int LastPlaytimeFinish { get; set; }

  public int LastVisitSeqType { get; set; }

  public int LastVisitCheckDay { get; set; }

  public long LastPlaytimeValue { get; set; }

  public uint LastVisitDate { get; set; }

  public uint LastPlaytimeDate { get; set; }

  public uint LastLoginDate { get; set; }

  public uint LastXmasDate
  {
    [CompilerGenerated, SpecialName] get => ((PlayerEvent) this).uint_3;
    [CompilerGenerated, SpecialName] set => ((PlayerEvent) this).uint_3 = value;
  }

  public uint LastQuestDate
  {
    [CompilerGenerated, SpecialName] get => ((PlayerEvent) this).uint_4;
    [CompilerGenerated, SpecialName] set => ((PlayerEvent) this).uint_4 = value;
  }
}
