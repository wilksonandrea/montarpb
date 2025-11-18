// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_START_COUNTDOWN_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_START_COUNTDOWN_ACK : GameServerPacket
{
  private readonly CountDownEnum countDownEnum_0;

  public virtual void Write()
  {
    this.WriteH((short) 5147);
    this.WriteB(((PROTOCOL_BATTLE_SENDPING_ACK) this).byte_0);
  }

  public PROTOCOL_BATTLE_START_COUNTDOWN_ACK(
    [In] SlotModel obj0,
    Account slotModel_1,
    [In] List<int> obj2,
    [In] bool obj3)
  {
    ((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).slotModel_0 = obj0;
    ((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0 = slotModel_1.Room;
    if (((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0 == null)
      return;
    ((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).bool_0 = obj3;
    ((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).list_0 = obj2;
    if (((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0.IsBotMode() || ((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0.RoomType == RoomCondition.Tutorial)
      return;
    AllUtils.CompleteMission(((PROTOCOL_BATTLE_STARTBATTLE_ACK) this).roomModel_0, slotModel_1, obj0, obj3 ? MissionType.STAGE_ENTER : MissionType.STAGE_INTERCEPT, 0);
  }
}
