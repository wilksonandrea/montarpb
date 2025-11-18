// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Models.Event.DropWeaponInfo
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

#nullable disable
namespace Server.Match.Data.Models.Event;

public class DropWeaponInfo
{
  public byte Flags;
  public byte Extensions;
  public byte Accessory;
  public byte Counter;
  public ushort AmmoPrin;
  public ushort AmmoDual;
  public ushort AmmoTotal;
  public ushort Unk1;
  public ushort Unk2;
  public ushort Unk3;
  public int WeaponId;
}
