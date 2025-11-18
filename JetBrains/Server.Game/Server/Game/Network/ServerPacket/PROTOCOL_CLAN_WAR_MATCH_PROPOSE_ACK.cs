// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CLAN_WAR_MATCH_PROPOSE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_MATCH_PROPOSE_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 1566);
    this.WriteD(((PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK) this).int_0);
    this.WriteH((ushort) ((PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK) this).int_1);
    this.WriteH((ushort) ((RoomModel) ((PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK) this).matchModel_0).GetServerInfo());
  }

  public PROTOCOL_CLAN_WAR_MATCH_PROPOSE_ACK(uint uint_1, MatchModel int_1 = 0)
  {
    ((PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK) this).uint_0 = uint_1;
    ((PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK) this).matchModel_0 = int_1;
  }
}
