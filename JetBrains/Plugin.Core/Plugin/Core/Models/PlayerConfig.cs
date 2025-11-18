// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.PlayerConfig
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class PlayerConfig
{
  public bool AddBonuses(int value)
  {
    int bonuses = ((PlayerBonus) this).Bonuses;
    int freePass = ((PlayerBonus) this).FreePass;
    switch (value)
    {
      case 1600001:
        this.method_1(1);
        break;
      case 1600002:
        this.method_1(2);
        break;
      case 1600003:
        this.method_1(4);
        break;
      case 1600004:
        this.method_1(16 /*0x10*/);
        break;
      case 1600011:
        this.method_3(128 /*0x80*/);
        break;
      case 1600037:
        this.method_1(8);
        break;
      case 1600038:
        this.method_1(64 /*0x40*/);
        break;
      case 1600119:
        this.method_1(32 /*0x20*/);
        break;
      case 1600201:
        this.method_1(512 /*0x0200*/);
        break;
      case 1600202:
        this.method_1(1024 /*0x0400*/);
        break;
      case 1600203:
        this.method_1(2048 /*0x0800*/);
        break;
      case 1600204:
        this.method_1(4096 /*0x1000*/);
        break;
    }
    return ((PlayerBonus) this).Bonuses != bonuses || ((PlayerBonus) this).FreePass != freePass;
  }

  private void method_0(int value)
  {
    ((PlayerBonus) this).Bonuses = ((PlayerBonus) this).Bonuses & ~value;
  }

  private void method_1(int value)
  {
    ((PlayerBonus) this).Bonuses = ((PlayerBonus) this).Bonuses | value;
  }

  private void method_2(int value)
  {
    ((PlayerBonus) this).FreePass = ((PlayerBonus) this).FreePass & ~value;
  }

  private void method_3(int ItemId)
  {
    ((PlayerBonus) this).FreePass = ((PlayerBonus) this).FreePass | ItemId;
  }

  public long OwnerId { get; set; }

  public int Crosshair { get; set; }

  public int AudioSFX { get; set; }

  public int AudioBGM { get; set; }

  public int Sensitivity { get; set; }

  public int PointOfView { get; set; }

  public int ShowBlood { get; set; }

  public int HandPosition { get; set; }

  public int AudioEnable { get; set; }

  public int Config { get; set; }

  public int InvertMouse { get; set; }

  public int EnableInviteMsg { get; set; }

  public int EnableWhisperMsg { get; set; }

  public int Macro { get; set; }

  public int Nations { get; set; }

  public string Macro1 { get; set; }

  public string Macro2 { get; set; }

  public string Macro3 { get; set; }

  public string Macro4 { get; set; }

  public string Macro5
  {
    [CompilerGenerated, SpecialName] get => ((PlayerConfig) this).string_4;
    [CompilerGenerated, SpecialName] set => ((PlayerConfig) this).string_4 = value;
  }

  public byte[] KeyboardKeys
  {
    [CompilerGenerated, SpecialName] get => ((PlayerConfig) this).byte_0;
    [CompilerGenerated, SpecialName] set => ((PlayerConfig) this).byte_0 = value;
  }
}
