// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.XML.CharaStructureXML
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Managers;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Data.XML;

public class CharaStructureXML
{
  public static List<CharaModel> Charas;

  public static void WriteInfo(SyncServerPacket S, WeaponRecoilInfo Infos)
  {
    S.WriteT(Infos.RecoilHorzAngle);
    S.WriteT(Infos.RecoilHorzMax);
    S.WriteT(Infos.RecoilVertAngle);
    S.WriteT(Infos.RecoilVertMax);
    S.WriteT(Infos.Deviation);
    S.WriteC(Infos.RecoilHorzCount);
    S.WriteD(Infos.WeaponId);
    S.WriteC(Infos.Accessory);
    S.WriteC(Infos.Extensions);
  }

  public CharaStructureXML()
  {
  }

  public static WeaponSyncInfo ReadInfo(
    ActionModel Action,
    SyncClientPacket C,
    bool GenLog,
    [In] bool obj3)
  {
    AssistManager assistManager = new AssistManager();
    ((WeaponSyncInfo) assistManager).WeaponId = C.ReadD();
    ((WeaponSyncInfo) assistManager).Accessory = C.ReadC();
    ((WeaponSyncInfo) assistManager).Extensions = C.ReadC();
    WeaponSyncInfo weaponSyncInfo = (WeaponSyncInfo) assistManager;
    if (!obj3)
      weaponSyncInfo.WeaponClass = (ClassType) ComDiv.GetIdStatics(weaponSyncInfo.WeaponId, 2);
    if (GenLog)
      CLogger.Print($"PVP Slot {Action.Slot}; Weapon Id: {weaponSyncInfo.WeaponId}; Extensions: {weaponSyncInfo.Extensions}; Unknowns: {weaponSyncInfo.Accessory}", LoggerType.Warning, (Exception) null);
    return weaponSyncInfo;
  }

  public static void WriteInfo([In] SyncServerPacket obj0, WeaponSyncInfo Info)
  {
    obj0.WriteD(Info.WeaponId);
    obj0.WriteC(Info.Accessory);
    obj0.WriteC(Info.Extensions);
  }

  public CharaStructureXML()
  {
  }

  public static void Load()
  {
    string path = "Data/Match/CharaHealth.xml";
    if (File.Exists(path))
      ItemStatisticXML.smethod_0(path);
    else
      CLogger.Print("File not found: " + path, LoggerType.Warning, (Exception) null);
  }
}
