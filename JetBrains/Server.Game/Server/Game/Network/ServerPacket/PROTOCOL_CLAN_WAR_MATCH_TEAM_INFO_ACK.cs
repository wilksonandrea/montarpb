// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK : GameServerPacket
{
  private readonly uint uint_0;
  private readonly ClanModel clanModel_0;
  private readonly Account account_0;
  private readonly int int_0;

  public virtual void Write()
  {
    this.WriteH((short) 6923);
    this.WriteD(((PROTOCOL_CLAN_WAR_LEAVE_TEAM_ACK) this).uint_0);
  }

  public PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK([In] uint obj0)
  {
    ((PROTOCOL_CLAN_WAR_MATCH_PROPOSE_ACK) this).uint_0 = obj0;
  }

  public virtual void Write()
  {
    this.WriteH((short) 1554);
    this.WriteD(((PROTOCOL_CLAN_WAR_MATCH_PROPOSE_ACK) this).uint_0);
  }
}
