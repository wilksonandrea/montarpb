// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Models.SubHead.GrenadeInfo
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using System;
using System.Net;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Data.Models.SubHead;

public class GrenadeInfo
{
  public byte Accessory;
  public byte Extensions;
  public byte Unk1;
  public byte Unk2;
  public byte Unk3;
  public byte Unk4;
  public int WeaponId;
  public int Unk5;
  public int Unk6;
  public int Unk7;
  public ClassType WeaponClass;
  public ushort ObjPosX;
  public ushort ObjPosY;
  public ushort ObjPosZ;
  public ushort BoomInfo;
  public ushort GrenadesCount;

  public bool RemovePlayer(IPEndPoint Slot, PacketModel Active, [In] string obj2)
  {
    if (ConfigLoader.UdpVersion != obj2)
    {
      if (ConfigLoader.IsTestMode)
        CLogger.Print($"Wrong UDP Version ({obj2}); Player can't be disconnected on it!", LoggerType.Warning, (Exception) null);
      return false;
    }
    try
    {
      PlayerModel player = ((RoomModel) this).Players[Active.Slot];
      if (player.CompareIp(Slot))
      {
        ((Equipment) player).ResetAllInfos();
        return true;
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
    return false;
  }
}
