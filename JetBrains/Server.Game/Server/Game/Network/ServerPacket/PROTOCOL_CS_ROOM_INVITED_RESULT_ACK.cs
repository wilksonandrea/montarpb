// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_ROOM_INVITED_RESULT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_ROOM_INVITED_RESULT_ACK : GameServerPacket
{
  private readonly long long_0;

  public PROTOCOL_CS_ROOM_INVITED_RESULT_ACK(
    int eventErrorEnum_1,
    [In] int obj1,
    [In] int obj2,
    [In] byte[] obj3)
  {
    ((PROTOCOL_CS_REQUEST_LIST_ACK) this).int_0 = eventErrorEnum_1;
    ((PROTOCOL_CS_REQUEST_LIST_ACK) this).int_2 = obj1;
    ((PROTOCOL_CS_REQUEST_LIST_ACK) this).int_1 = obj2;
    ((PROTOCOL_CS_REQUEST_LIST_ACK) this).byte_0 = obj3;
  }

  public PROTOCOL_CS_ROOM_INVITED_RESULT_ACK([In] int obj0)
  {
    ((PROTOCOL_CS_REQUEST_LIST_ACK) this).int_0 = obj0;
  }
}
