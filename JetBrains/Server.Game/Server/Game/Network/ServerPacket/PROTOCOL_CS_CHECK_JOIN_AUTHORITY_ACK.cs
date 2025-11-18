// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ACK([In] string obj0, [In] Account obj1)
  {
    ((PROTOCOL_CS_CHATTING_ACK) this).string_0 = obj0;
    ((PROTOCOL_CS_CHATTING_ACK) this).account_0 = obj1;
  }

  public PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ACK([In] int obj0, int int_1)
  {
    ((PROTOCOL_CS_CHATTING_ACK) this).int_0 = obj0;
    ((PROTOCOL_CS_CHATTING_ACK) this).int_1 = int_1;
  }
}
