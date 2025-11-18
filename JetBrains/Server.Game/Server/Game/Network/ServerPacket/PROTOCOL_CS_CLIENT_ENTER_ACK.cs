// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_CLIENT_ENTER_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_CLIENT_ENTER_ACK : GameServerPacket
{
  private readonly int int_0;
  private readonly int int_1;

  public virtual void Write()
  {
    this.WriteH((short) 1955);
    this.WriteC((byte) ((PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_ACK) this).int_0);
    this.WriteC((byte) 13);
    this.WriteC((byte) Math.Ceiling((double) ((PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_ACK) this).int_0 / 13.0));
  }

  public PROTOCOL_CS_CLIENT_ENTER_ACK(int uint_1, [In] List<MatchModel> obj1)
  {
    ((PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_ACK) this).int_0 = uint_1;
    ((PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_ACK) this).list_0 = obj1;
  }
}
