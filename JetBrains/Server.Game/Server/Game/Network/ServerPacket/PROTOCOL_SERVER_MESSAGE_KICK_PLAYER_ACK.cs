// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_SERVER_MESSAGE_KICK_PLAYER_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SERVER_MESSAGE_KICK_PLAYER_ACK : GameServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 3077);
    this.WriteU(((PROTOCOL_SERVER_MESSAGE_INVITED_ACK) this).account_0.Nickname, 66);
    this.WriteD(((PROTOCOL_SERVER_MESSAGE_INVITED_ACK) this).roomModel_0.RoomId);
    this.WriteQ(((PROTOCOL_SERVER_MESSAGE_INVITED_ACK) this).account_0.PlayerId);
    this.WriteS(((PROTOCOL_SERVER_MESSAGE_INVITED_ACK) this).roomModel_0.Password, 4);
  }

  public PROTOCOL_SERVER_MESSAGE_KICK_PLAYER_ACK(uint uint_1)
  {
    ((PROTOCOL_SERVER_MESSAGE_ITEM_RECEIVE_ACK) this).uint_0 = uint_1;
  }
}
