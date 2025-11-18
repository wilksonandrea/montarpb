// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK : GameServerPacket
{
  private readonly uint uint_0;
  private readonly MatchModel matchModel_0;

  public virtual void Write() => this.WriteH((short) 6935);

  public PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(MatchModel uint_1, Account byte_1)
  {
    ((PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK) this).match = uint_1;
    ((PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK) this).Player = byte_1;
  }
}
