// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_RANDOMBOX_LIST_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Utility;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_RANDOMBOX_LIST_ACK : GameServerPacket
{
  private readonly bool bool_0;

  public virtual void Write()
  {
    this.WriteH((short) 2367);
    this.WriteD(((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK) this).uint_0);
    if (((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK) this).uint_0 != 0U)
      return;
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK) this).account_0.Mission.ActualMission);
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK) this).account_0.Mission.Card1);
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK) this).account_0.Mission.Card2);
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK) this).account_0.Mission.Card3);
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK) this).account_0.Mission.Card4);
    this.WriteB(ComDiv.GetMissionCardFlags(((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK) this).account_0.Mission.Mission1, ((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK) this).account_0.Mission.List1));
    this.WriteB(ComDiv.GetMissionCardFlags(((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK) this).account_0.Mission.Mission2, ((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK) this).account_0.Mission.List2));
    this.WriteB(ComDiv.GetMissionCardFlags(((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK) this).account_0.Mission.Mission3, ((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK) this).account_0.Mission.List3));
    this.WriteB(ComDiv.GetMissionCardFlags(((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK) this).account_0.Mission.Mission4, ((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK) this).account_0.Mission.List4));
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK) this).account_0.Mission.Mission1);
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK) this).account_0.Mission.Mission2);
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK) this).account_0.Mission.Mission3);
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_DELETE_CARD_SET_ACK) this).account_0.Mission.Mission4);
  }

  public PROTOCOL_BASE_RANDOMBOX_LIST_ACK([In] Account obj0)
  {
    ((PROTOCOL_BASE_QUEST_GET_INFO_ACK) this).account_0 = obj0;
  }
}
