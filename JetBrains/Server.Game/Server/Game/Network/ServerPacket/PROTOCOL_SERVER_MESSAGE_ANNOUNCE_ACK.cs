// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK : GameServerPacket
{
  private readonly string string_0;

  public virtual void Write()
  {
    this.WriteH((short) 8456);
    this.WriteH((short) 0);
    this.WriteD(((PROTOCOL_SEASON_CHALLENGE_PLUS_SEASON_EXP_ACK) this).uint_0);
    this.WriteC((byte) 1);
    this.WriteC((byte) 6);
    this.WriteD(2580);
    this.WriteC((byte) 5);
    this.WriteC((byte) 5);
    this.WriteC((byte) 1);
  }
}
