// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_MEMBER_INFO_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_MEMBER_INFO_ACK : GameServerPacket
{
  private readonly List<Account> list_0;

  public PROTOCOL_CS_MEMBER_INFO_ACK(uint int_2, int clanModel_1)
  {
    ((PROTOCOL_CS_JOIN_REQUEST_ACK) this).uint_0 = int_2;
    ((PROTOCOL_CS_JOIN_REQUEST_ACK) this).int_0 = clanModel_1;
  }

  public virtual void Write()
  {
    this.WriteH((short) 813);
    this.WriteD(((PROTOCOL_CS_JOIN_REQUEST_ACK) this).uint_0);
    if (((PROTOCOL_CS_JOIN_REQUEST_ACK) this).uint_0 != 0U)
      return;
    this.WriteD(((PROTOCOL_CS_JOIN_REQUEST_ACK) this).int_0);
  }
}
