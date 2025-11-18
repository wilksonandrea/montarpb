// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.SubHead.StageInfoObjAnim
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
namespace Server.Match.Network.Actions.SubHead;

public class StageInfoObjAnim
{
  public static void WriteInfo([In] SyncServerPacket obj0, ObjectStaticInfo GenLog)
  {
    obj0.WriteH(GenLog.Type);
    obj0.WriteH(GenLog.Life);
    obj0.WriteC(GenLog.DestroyedBySlot);
  }

  public static StageStaticInfo ReadInfo([In] SyncClientPacket obj0, bool Info)
  {
    ActionStateInfo actionStateInfo = new ActionStateInfo();
    ((StageStaticInfo) actionStateInfo).IsDestroyed = obj0.ReadC();
    StageStaticInfo stageStaticInfo = (StageStaticInfo) actionStateInfo;
    if (Info)
      CLogger.Print($"Sub Head: StageInfoObjStatic; Destroyed: {stageStaticInfo.IsDestroyed}", LoggerType.Warning, (Exception) null);
    return stageStaticInfo;
  }
}
