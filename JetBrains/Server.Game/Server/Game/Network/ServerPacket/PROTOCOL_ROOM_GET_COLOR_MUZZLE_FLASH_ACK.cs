// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK : GameServerPacket
{
  private readonly int int_0;
  private readonly int int_1;

  public virtual void Write()
  {
    this.WriteH((short) 3606);
    this.WriteH((short) ((PROTOCOL_ROOM_CHATTING_ACK) this).int_0);
    this.WriteD(((PROTOCOL_ROOM_CHATTING_ACK) this).int_1);
    this.WriteC((byte) ((PROTOCOL_ROOM_CHATTING_ACK) this).bool_0);
    this.WriteD(((PROTOCOL_ROOM_CHATTING_ACK) this).string_0.Length + 1);
    this.WriteN(((PROTOCOL_ROOM_CHATTING_ACK) this).string_0, ((PROTOCOL_ROOM_CHATTING_ACK) this).string_0.Length + 2, "UTF-16LE");
  }

  public PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK(uint uint_1)
  {
    ((PROTOCOL_ROOM_CHECK_MAIN_ACK) this).uint_0 = uint_1;
  }
}
