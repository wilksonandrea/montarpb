// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.SubHead.ObjectStatic
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models.Event;
using Server.Match.Data.Models.SubHead;
using System;

#nullable disable
namespace Server.Match.Network.Actions.SubHead;

public class ObjectStatic
{
  public static void WriteInfo(SyncServerPacket S, GrenadeInfo Info)
  {
    S.WriteC(Info.Unk1);
    S.WriteC(Info.Unk2);
    S.WriteC(Info.Unk3);
    S.WriteC(Info.Unk4);
    S.WriteH(Info.BoomInfo);
    S.WriteD(Info.WeaponId);
    S.WriteC(Info.Accessory);
    S.WriteC(Info.Extensions);
    S.WriteH(Info.ObjPosX);
    S.WriteH(Info.ObjPosY);
    S.WriteH(Info.ObjPosZ);
    S.WriteD(Info.Unk5);
    S.WriteD(Info.Unk6);
    S.WriteD(Info.Unk7);
  }

  public static ObjectAnimInfo ReadInfo(SyncClientPacket C, bool GenLog)
  {
    SeizeDataForClientInfo dataForClientInfo = new SeizeDataForClientInfo();
    ((ObjectAnimInfo) dataForClientInfo).Life = C.ReadUH();
    ((ObjectAnimInfo) dataForClientInfo).Anim1 = C.ReadC();
    ((ObjectAnimInfo) dataForClientInfo).Anim2 = C.ReadC();
    ((ObjectAnimInfo) dataForClientInfo).SyncDate = C.ReadT();
    ObjectAnimInfo objectAnimInfo = (ObjectAnimInfo) dataForClientInfo;
    if (GenLog)
      CLogger.Print($"Sub Head: ObjectAnim; Life: {objectAnimInfo.Life}; Animation[1]: {objectAnimInfo.Anim1}; Animation[2]: {objectAnimInfo.Anim2}; Sync: {objectAnimInfo.SyncDate}", LoggerType.Warning, (Exception) null);
    return objectAnimInfo;
  }
}
