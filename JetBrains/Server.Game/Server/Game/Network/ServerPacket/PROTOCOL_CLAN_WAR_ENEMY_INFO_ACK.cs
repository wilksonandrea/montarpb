// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK : GameServerPacket
{
  public readonly MatchModel match;

  public virtual void Write()
  {
    this.WriteH((short) 6927);
    this.WriteH((short) ((PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK) this).match.MatchId);
    this.WriteH((ushort) ((RoomModel) ((PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK) this).match).GetServerInfo());
    this.WriteH((ushort) ((RoomModel) ((PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK) this).match).GetServerInfo());
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK) this).match.State);
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK) this).match.FriendId);
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK) this).match.Training);
    this.WriteC((byte) ((RoomModel) ((PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK) this).match).GetCountPlayers());
    this.WriteD(((PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK) this).match.Leader);
    this.WriteC((byte) 0);
    this.WriteD(((PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK) this).match.Clan.Id);
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK) this).match.Clan.Rank);
    this.WriteD(((PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK) this).match.Clan.Logo);
    this.WriteS(((PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK) this).match.Clan.Name, 17);
    this.WriteT(((PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK) this).match.Clan.Points);
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK) this).match.Clan.NameColor);
    if (((PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK) this).Player != null)
    {
      this.WriteC((byte) ((PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK) this).Player.Rank);
      this.WriteS(((PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK) this).Player.Nickname, 33);
      this.WriteQ(((PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK) this).Player.PlayerId);
      this.WriteC((byte) ((PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK) this).match.Slots[((PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK) this).match.Leader].State);
    }
    else
      this.WriteB(new byte[43]);
  }

  public PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK([In] MatchModel obj0)
  {
    ((PROTOCOL_CLAN_WAR_CREATE_ROOM_ACK) this).match = obj0;
  }
}
