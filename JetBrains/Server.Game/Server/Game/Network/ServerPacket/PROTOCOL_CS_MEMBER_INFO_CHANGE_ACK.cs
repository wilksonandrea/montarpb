// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Utility;
using Server.Game.Data.Models;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK : GameServerPacket
{
  private readonly Account account_0;
  private readonly ulong ulong_0;

  public PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK(int account_1, [In] int obj1)
  {
    ((PROTOCOL_CS_MEMBER_CONTEXT_ACK) this).int_0 = account_1;
    ((PROTOCOL_CS_MEMBER_CONTEXT_ACK) this).int_1 = obj1;
  }

  public PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK(int uint_1)
  {
    ((PROTOCOL_CS_MEMBER_CONTEXT_ACK) this).int_0 = uint_1;
  }

  public virtual void Write()
  {
    this.WriteH((short) 803);
    this.WriteD(((PROTOCOL_CS_MEMBER_CONTEXT_ACK) this).int_0);
    if (((PROTOCOL_CS_MEMBER_CONTEXT_ACK) this).int_0 != 0)
      return;
    this.WriteC((byte) ((PROTOCOL_CS_MEMBER_CONTEXT_ACK) this).int_1);
    this.WriteC((byte) 14);
    this.WriteC((byte) Math.Ceiling((double) ((PROTOCOL_CS_MEMBER_CONTEXT_ACK) this).int_1 / 14.0));
    this.WriteD(uint.Parse(DateTimeUtil.Now("MMddHHmmss")));
  }
}
