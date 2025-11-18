// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_AUTH_FRIEND_ACCEPT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_FRIEND_ACCEPT_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 1989);
    this.WriteC((byte) ((PROTOCOL_AUTH_ACCOUNT_KICK_ACK) this).int_0);
  }

  public PROTOCOL_AUTH_FRIEND_ACCEPT_ACK(string uint_1)
  {
    ((PROTOCOL_AUTH_CHANGE_NICKNAME_ACK) this).string_0 = uint_1;
  }
}
