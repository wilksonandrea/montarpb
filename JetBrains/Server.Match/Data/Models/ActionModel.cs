// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Models.ActionModel
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Server.Match.Data.Enums;
using System.Runtime.CompilerServices;

#nullable disable
namespace Server.Match.Data.Models;

public class ActionModel
{
  [CompilerGenerated]
  [SpecialName]
  public float get_FireDelay() => ((ItemsStatistic) this).float_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_FireDelay(float value) => ((ItemsStatistic) this).float_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public float get_Range() => ((ItemsStatistic) this).float_1;

  [CompilerGenerated]
  [SpecialName]
  public void set_Range(float value) => ((ItemsStatistic) this).float_1 = value;

  public ushort Slot { get; set; }

  public ushort Length { get; set; }

  public UdpGameEvent Flag { get; set; }

  public UdpSubHead SubHead
  {
    [CompilerGenerated, SpecialName] get => ((ActionModel) this).udpSubHead_0;
    [CompilerGenerated, SpecialName] set => ((ActionModel) this).udpSubHead_0 = value;
  }

  public byte[] Data
  {
    [CompilerGenerated, SpecialName] get => ((ActionModel) this).byte_0;
    [CompilerGenerated, SpecialName] set => ((ActionModel) this).byte_0 = value;
  }
}
