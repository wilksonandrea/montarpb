// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CLAN_WAR_MATCH_PROPOSE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CLAN_WAR_MATCH_PROPOSE_REQ : GameClientPacket
{
  private int int_0;
  private int int_1;
  private uint uint_0;

  private void method_0(Account roomModel_0, MatchModel bool_0)
  {
    if (!bool_0.AddPlayer(roomModel_0))
      ((PROTOCOL_CLAN_WAR_JOIN_TEAM_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
    this.Client.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_MATCH_PROPOSE_ACK(((PROTOCOL_CLAN_WAR_JOIN_TEAM_REQ) this).uint_0, bool_0));
    if (((PROTOCOL_CLAN_WAR_JOIN_TEAM_REQ) this).uint_0 != 0U)
      return;
    using (PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK registMercenaryAck = (PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK) new PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK(bool_0))
      bool_0.SendPacketToPlayers((GameServerPacket) registMercenaryAck);
  }

  public virtual void Read()
  {
  }
}
