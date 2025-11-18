// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CLAN_WAR_INVITE_ACCEPT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_INVITE_ACCEPT_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 1564);
    this.WriteH((short) ((PROTOCOL_CLAN_WAR_CREATE_ROOM_ACK) this).match.MatchId);
    this.WriteD(((RoomModel) ((PROTOCOL_CLAN_WAR_CREATE_ROOM_ACK) this).match).GetServerInfo());
    this.WriteH((short) ((RoomModel) ((PROTOCOL_CLAN_WAR_CREATE_ROOM_ACK) this).match).GetServerInfo());
    this.WriteC((byte) 10);
  }

  public PROTOCOL_CLAN_WAR_INVITE_ACCEPT_ACK([In] uint obj0, [In] MatchModel obj1)
  {
    ((PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK) this).uint_0 = obj0;
    ((PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK) this).matchModel_0 = obj1;
  }
}
