// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_QUEST_GET_INFO_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_QUEST_GET_INFO_ACK : GameServerPacket
{
  private readonly Account account_0;

  public virtual void Write()
  {
    this.WriteH((short) 2365);
    this.WriteD((uint) ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK) this).eventErrorEnum_0);
    if (((PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK) this).eventErrorEnum_0 != EventErrorEnum.SUCCESS)
      return;
    this.WriteD(((PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK) this).account_0.Gold);
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK) this).account_0.Mission.ActualMission);
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK) this).account_0.Mission.ActualMission);
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK) this).account_0.Mission.Card1);
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK) this).account_0.Mission.Card2);
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK) this).account_0.Mission.Card3);
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK) this).account_0.Mission.Card4);
    this.WriteB(ComDiv.GetMissionCardFlags(((PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK) this).account_0.Mission.Mission1, ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK) this).account_0.Mission.List1));
    this.WriteB(ComDiv.GetMissionCardFlags(((PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK) this).account_0.Mission.Mission2, ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK) this).account_0.Mission.List2));
    this.WriteB(ComDiv.GetMissionCardFlags(((PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK) this).account_0.Mission.Mission3, ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK) this).account_0.Mission.List3));
    this.WriteB(ComDiv.GetMissionCardFlags(((PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK) this).account_0.Mission.Mission4, ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK) this).account_0.Mission.List4));
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK) this).account_0.Mission.Mission1);
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK) this).account_0.Mission.Mission2);
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK) this).account_0.Mission.Mission3);
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK) this).account_0.Mission.Mission4);
    this.WriteD(((PROTOCOL_BASE_QUEST_BUY_CARD_SET_ACK) this).account_0.Cash);
  }

  public PROTOCOL_BASE_QUEST_GET_INFO_ACK(int uint_1, MissionCardModel int_1)
  {
    ((PROTOCOL_BASE_QUEST_CHANGE_ACK) this).int_0 = int_1.MissionBasicId;
    if (int_1.MissionLimit == uint_1)
      ((PROTOCOL_BASE_QUEST_CHANGE_ACK) this).int_0 = ((PROTOCOL_BASE_QUEST_CHANGE_ACK) this).int_0 + 240 /*0xF0*/;
    ((PROTOCOL_BASE_QUEST_CHANGE_ACK) this).int_1 = uint_1;
  }
}
