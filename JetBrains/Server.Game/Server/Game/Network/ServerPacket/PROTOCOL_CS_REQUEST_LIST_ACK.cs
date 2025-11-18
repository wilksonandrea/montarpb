// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_REQUEST_LIST_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.SQL;
using Plugin.Core.Utility;
using System;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_REQUEST_LIST_ACK : GameServerPacket
{
  private readonly int int_0;
  private readonly int int_1;
  private readonly int int_2;
  private readonly byte[] byte_0;

  public virtual void Write()
  {
    this.WriteH((short) 873);
    this.WriteD(0);
    this.WriteC((byte) ((PROTOCOL_CS_REPLACE_PERSONMAX_ACK) this).int_0);
  }

  public PROTOCOL_CS_REQUEST_LIST_ACK(int uint_1)
  {
    if (uint_1 > 0)
      ((PROTOCOL_CS_REQUEST_CONTEXT_ACK) this).int_0 = DaoManagerSQL.GetRequestClanInviteCount(uint_1);
    else
      ((PROTOCOL_CS_REQUEST_CONTEXT_ACK) this).uint_0 = uint.MaxValue;
  }

  public virtual void Write()
  {
    this.WriteH((short) 817);
    this.WriteD(((PROTOCOL_CS_REQUEST_CONTEXT_ACK) this).uint_0);
    if (((PROTOCOL_CS_REQUEST_CONTEXT_ACK) this).uint_0 != 0U)
      return;
    this.WriteC((byte) ((PROTOCOL_CS_REQUEST_CONTEXT_ACK) this).int_0);
    this.WriteC((byte) 13);
    this.WriteC((byte) Math.Ceiling((double) ((PROTOCOL_CS_REQUEST_CONTEXT_ACK) this).int_0 / 13.0));
    this.WriteD(uint.Parse(DateTimeUtil.Now("MMddHHmmss")));
  }
}
