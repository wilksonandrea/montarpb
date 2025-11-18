// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK()
  {
  }

  public virtual void Write()
  {
    this.WriteH((short) 3610);
    this.WriteC((byte) 2);
    this.WriteC((byte) 0);
    this.WriteC((byte) 0);
    this.WriteC((byte) 0);
    this.WriteC((byte) 0);
    this.WriteC((byte) 2);
    this.WriteC((byte) 0);
    this.WriteC((byte) 8);
  }

  public PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK(uint uint_1)
  {
    ((PROTOCOL_ROOM_REQUEST_MAIN_ACK) this).uint_0 = uint_1;
  }
}
