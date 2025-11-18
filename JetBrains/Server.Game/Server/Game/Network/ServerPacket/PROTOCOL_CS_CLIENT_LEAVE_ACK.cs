// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_CLIENT_LEAVE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_CLIENT_LEAVE_ACK : GameServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 1957);
    this.WriteC(((PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_ACK) this).int_0 == 0 ? (byte) ((PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_ACK) this).list_0.Count : (byte) ((PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_ACK) this).int_0);
    if (((PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_ACK) this).int_0 > 0 || ((PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_ACK) this).list_0.Count == 0)
      return;
    this.WriteC((byte) 1);
    this.WriteC((byte) 0);
    this.WriteC((byte) ((PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_ACK) this).list_0.Count);
    for (int index = 0; index < ((PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_ACK) this).list_0.Count; ++index)
    {
      MatchModel matchModel = ((PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_ACK) this).list_0[index];
      this.WriteH((short) matchModel.MatchId);
      this.WriteH((ushort) ((RoomModel) matchModel).GetServerInfo());
      this.WriteH((ushort) ((RoomModel) matchModel).GetServerInfo());
      this.WriteC((byte) matchModel.State);
      this.WriteC((byte) matchModel.FriendId);
      this.WriteC((byte) matchModel.Training);
      this.WriteC((byte) ((RoomModel) matchModel).GetCountPlayers());
      this.WriteC((byte) 0);
      this.WriteD(matchModel.Leader);
      Account leader = ((RoomModel) matchModel).GetLeader();
      if (leader != null)
      {
        this.WriteC((byte) leader.Rank);
        this.WriteU(leader.Nickname, 66);
        this.WriteQ(leader.PlayerId);
        this.WriteC((byte) matchModel.Slots[matchModel.Leader].State);
      }
      else
        this.WriteB(new byte[76]);
    }
  }

  public PROTOCOL_CS_CLIENT_LEAVE_ACK([In] byte obj0, int clanModel_1, [In] byte[] obj2)
  {
    ((PROTOCOL_CS_CLAN_LIST_FILTER_ACK) this).byte_0 = obj0;
    ((PROTOCOL_CS_CLAN_LIST_FILTER_ACK) this).int_0 = clanModel_1;
    ((PROTOCOL_CS_CLAN_LIST_FILTER_ACK) this).byte_1 = obj2;
  }
}
