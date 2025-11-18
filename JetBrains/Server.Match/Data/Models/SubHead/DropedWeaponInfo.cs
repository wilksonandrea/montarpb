// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Models.SubHead.DropedWeaponInfo
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core.SharpDX;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Data.Models.SubHead;

public class DropedWeaponInfo
{
  public byte WeaponFlag;
  public ushort Unk1;
  public ushort Unk2;
  public ushort Unk3;
  public ushort Unk4;
  public Half3 WeaponPos;
  public byte[] Unks;

  public ObjectInfo GetObject([In] int obj0)
  {
    try
    {
      return ((RoomModel) this).Objects[obj0];
    }
    catch
    {
      return (ObjectInfo) null;
    }
  }
}
