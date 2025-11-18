// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_SUPPLAY_BOX_ANNOUNCE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_SUPPLAY_BOX_ANNOUNCE_ACK : GameServerPacket
{
  private readonly string string_0;

  public virtual void Write()
  {
    this.WriteH((short) 2355);
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_GET_INFO_ACK) this).account_0.Mission.ActualMission);
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_GET_INFO_ACK) this).account_0.Mission.ActualMission);
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_GET_INFO_ACK) this).account_0.Mission.Card1);
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_GET_INFO_ACK) this).account_0.Mission.Card2);
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_GET_INFO_ACK) this).account_0.Mission.Card3);
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_GET_INFO_ACK) this).account_0.Mission.Card4);
    this.WriteB(ComDiv.GetMissionCardFlags(((PROTOCOL_BASE_QUEST_GET_INFO_ACK) this).account_0.Mission.Mission1, ((PROTOCOL_BASE_QUEST_GET_INFO_ACK) this).account_0.Mission.List1));
    this.WriteB(ComDiv.GetMissionCardFlags(((PROTOCOL_BASE_QUEST_GET_INFO_ACK) this).account_0.Mission.Mission2, ((PROTOCOL_BASE_QUEST_GET_INFO_ACK) this).account_0.Mission.List2));
    this.WriteB(ComDiv.GetMissionCardFlags(((PROTOCOL_BASE_QUEST_GET_INFO_ACK) this).account_0.Mission.Mission3, ((PROTOCOL_BASE_QUEST_GET_INFO_ACK) this).account_0.Mission.List3));
    this.WriteB(ComDiv.GetMissionCardFlags(((PROTOCOL_BASE_QUEST_GET_INFO_ACK) this).account_0.Mission.Mission4, ((PROTOCOL_BASE_QUEST_GET_INFO_ACK) this).account_0.Mission.List4));
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_GET_INFO_ACK) this).account_0.Mission.Mission1);
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_GET_INFO_ACK) this).account_0.Mission.Mission2);
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_GET_INFO_ACK) this).account_0.Mission.Mission3);
    this.WriteC((byte) ((PROTOCOL_BASE_QUEST_GET_INFO_ACK) this).account_0.Mission.Mission4);
  }

  public PROTOCOL_BASE_SUPPLAY_BOX_ANNOUNCE_ACK(PlayerSession int_2)
  {
    ((PROTOCOL_BASE_GET_USER_SUBTASK_ACK) this).account_0 = ClanManager.GetAccount(int_2.PlayerId, true);
    ((PROTOCOL_BASE_GET_USER_SUBTASK_ACK) this).int_0 = int_2.SessionId;
  }
}
