// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.SubHead.DropedWeapon
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Models.SubHead;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Network.Actions.SubHead;

public class DropedWeapon
{
  public static void WriteInfo(SyncServerPacket object_0, [In] ObjectMoveInfo obj1)
  {
    object_0.WriteB(obj1.Unk);
  }

  public static StageControlInfo ReadInfo(SyncClientPacket Objs, [In] bool obj1)
  {
    StageAnimInfo stageAnimInfo = new StageAnimInfo();
    ((StageControlInfo) stageAnimInfo).Unk = Objs.ReadB(9);
    StageControlInfo stageControlInfo = (StageControlInfo) stageAnimInfo;
    if (obj1)
      CLogger.Print("Sub Head: StageInfoObjControl; " + Bitwise.ToHexData("Controled Object Hex Data", stageControlInfo.Unk), LoggerType.Opcode, (Exception) null);
    return stageControlInfo;
  }
}
