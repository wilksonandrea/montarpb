// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.Event.ActionForObjectSync
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models.Event;
using Server.Match.Data.Models.SubHead;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Network.Actions.Event;

public class ActionForObjectSync
{
  public static void WriteInfo([In] SyncServerPacket obj0, StageStaticInfo GenLog)
  {
    obj0.WriteC(GenLog.IsDestroyed);
  }

  public static StageAnimInfo ReadInfo([In] SyncClientPacket obj0, bool Info)
  {
    WeaponRecoilInfo weaponRecoilInfo = new WeaponRecoilInfo();
    ((StageAnimInfo) weaponRecoilInfo).Unk = obj0.ReadC();
    ((StageAnimInfo) weaponRecoilInfo).Life = obj0.ReadUH();
    ((StageAnimInfo) weaponRecoilInfo).SyncDate = obj0.ReadT();
    ((StageAnimInfo) weaponRecoilInfo).Anim1 = obj0.ReadC();
    ((StageAnimInfo) weaponRecoilInfo).Anim2 = obj0.ReadC();
    StageAnimInfo stageAnimInfo = (StageAnimInfo) weaponRecoilInfo;
    if (Info)
      CLogger.Print($"Sub Head: StageObjAnim; Unk: {stageAnimInfo.Unk}; Life: {stageAnimInfo.Life}; Sync: {stageAnimInfo.SyncDate}; Animation[1]: {stageAnimInfo.Anim1}; Animation[2]: {stageAnimInfo.Anim2}", LoggerType.Warning, (Exception) null);
    return stageAnimInfo;
  }
}
