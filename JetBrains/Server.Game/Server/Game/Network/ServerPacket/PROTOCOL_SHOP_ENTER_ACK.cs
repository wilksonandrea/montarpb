// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_SHOP_ENTER_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SHOP_ENTER_ACK : GameServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 3076);
    this.WriteD((uint) ((PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK) this).eventErrorEnum_0);
  }
}
