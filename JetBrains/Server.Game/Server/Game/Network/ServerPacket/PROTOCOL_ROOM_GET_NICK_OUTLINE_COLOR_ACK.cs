// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK : GameServerPacket
{
  private readonly int int_0;
  private readonly int int_1;

  public virtual void Write()
  {
    this.WriteH((short) 3670);
    this.WriteD(((PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK) this).int_0);
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK) this).int_1);
  }

  public PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK(int uint_1, [In] int obj1)
  {
    ((PROTOCOL_ROOM_GET_COLOR_NICK_ACK) this).int_0 = uint_1;
    ((PROTOCOL_ROOM_GET_COLOR_NICK_ACK) this).int_1 = obj1;
  }
}
