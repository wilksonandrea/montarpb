// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Models.AnimModel
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Server.Match.Data.Enums;
using System.Runtime.CompilerServices;

#nullable disable
namespace Server.Match.Data.Models;

public class AnimModel
{
  [CompilerGenerated]
  [SpecialName]
  public UdpSubHead get_SubHead() => ((ActionModel) this).udpSubHead_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_SubHead(UdpSubHead value) => ((ActionModel) this).udpSubHead_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public byte[] get_Data() => ((ActionModel) this).byte_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Data(byte[] value) => ((ActionModel) this).byte_0 = value;

  public int Id { get; set; }

  public int NextAnim { get; set; }

  public int OtherObj { get; set; }

  public int OtherAnim
  {
    [CompilerGenerated, SpecialName] get => ((AnimModel) this).int_3;
    [CompilerGenerated, SpecialName] set => ((AnimModel) this).int_3 = value;
  }

  public float Duration
  {
    [CompilerGenerated, SpecialName] get => ((AnimModel) this).float_0;
    [CompilerGenerated, SpecialName] set => ((AnimModel) this).float_0 = value;
  }
}
