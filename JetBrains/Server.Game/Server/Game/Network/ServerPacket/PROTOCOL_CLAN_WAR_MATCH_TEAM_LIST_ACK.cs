// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CLAN_WAR_MATCH_TEAM_LIST_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_MATCH_TEAM_LIST_ACK : GameServerPacket
{
  private readonly List<MatchModel> list_0;
  private readonly int int_0;
  private readonly int int_1;

  public PROTOCOL_CLAN_WAR_MATCH_TEAM_LIST_ACK([In] int obj0)
  {
    ((PROTOCOL_CLAN_WAR_MATCH_TEAM_COUNT_ACK) this).int_0 = obj0;
  }

  public virtual void Write()
  {
    this.WriteH((short) 6915);
    this.WriteH((short) ((PROTOCOL_CLAN_WAR_MATCH_TEAM_COUNT_ACK) this).int_0);
    this.WriteC((byte) 13);
    this.WriteH((short) Math.Ceiling((double) ((PROTOCOL_CLAN_WAR_MATCH_TEAM_COUNT_ACK) this).int_0 / 13.0));
  }
}
