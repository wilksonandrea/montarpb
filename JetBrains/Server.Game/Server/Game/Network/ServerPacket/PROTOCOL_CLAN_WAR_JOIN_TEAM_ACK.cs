// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK : GameServerPacket
{
  private readonly MatchModel matchModel_0;
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 1560);
    this.WriteD(((PROTOCOL_CLAN_WAR_INVITE_ACCEPT_ACK) this).uint_0);
  }

  public PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK(uint matchModel_0, [In] int obj1)
  {
    ((PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK) this).uint_0 = matchModel_0;
    ((PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK) this).int_0 = obj1;
  }
}
