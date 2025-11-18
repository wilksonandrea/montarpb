// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CLAN_WAR_LEAVE_TEAM_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_LEAVE_TEAM_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 1572);
    this.WriteD(((PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK) this).uint_0);
    if (((PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK) this).uint_0 != 0U)
      return;
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK) this).int_0);
  }

  public PROTOCOL_CLAN_WAR_LEAVE_TEAM_ACK([In] MatchModel obj0, int matchModel_1 = default (int), [In] int obj2)
  {
    ((PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK) this).matchModel_0 = obj0;
    ((PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK) this).int_0 = matchModel_1;
    ((PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK) this).int_1 = obj2;
  }
}
