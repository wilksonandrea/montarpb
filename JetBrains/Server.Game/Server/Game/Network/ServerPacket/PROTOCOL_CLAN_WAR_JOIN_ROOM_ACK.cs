// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK : GameServerPacket
{
  private readonly MatchModel matchModel_0;
  private readonly int int_0;
  private readonly int int_1;

  public virtual void Write()
  {
    this.WriteH((short) 1574);
    this.WriteH((short) ((RoomModel) ((PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK) this).match).GetServerInfo());
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK) this).match.MatchId);
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK) this).match.FriendId);
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK) this).match.Training);
    this.WriteC((byte) ((RoomModel) ((PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK) this).match).GetCountPlayers());
    this.WriteD(((PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK) this).match.Leader);
    this.WriteC((byte) 0);
    this.WriteD(((PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK) this).match.Clan.Id);
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK) this).match.Clan.Rank);
    this.WriteD(((PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK) this).match.Clan.Logo);
    this.WriteS(((PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK) this).match.Clan.Name, 17);
    this.WriteT(((PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK) this).match.Clan.Points);
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK) this).match.Clan.NameColor);
  }

  public PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK([In] uint obj0)
  {
    ((PROTOCOL_CLAN_WAR_INVITE_ACCEPT_ACK) this).uint_0 = obj0;
  }
}
