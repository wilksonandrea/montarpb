// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Models.Event.SuicideInfo
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core.Enums;
using Plugin.Core.SharpDX;

#nullable disable
namespace Server.Match.Data.Models.Event;

public class SuicideInfo
{
  public uint HitInfo;
  public Half3 PlayerPos;
  public ClassType WeaponClass;
  public byte Extensions;
  public byte Accessory;
  public int WeaponId;
}
