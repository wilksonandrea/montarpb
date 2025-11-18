// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_GET_COLOR_NICK_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_GET_COLOR_NICK_ACK : GameServerPacket
{
  private readonly int int_0;
  private readonly int int_1;

  public virtual void Write()
  {
    this.WriteH((short) 3618);
    this.WriteD(((PROTOCOL_ROOM_CHECK_MAIN_ACK) this).uint_0);
  }

  public PROTOCOL_ROOM_GET_COLOR_NICK_ACK(uint int_2, RoomModel int_3)
  {
    ((PROTOCOL_ROOM_CREATE_ACK) this).uint_0 = int_2;
    ((PROTOCOL_ROOM_CREATE_ACK) this).roomModel_0 = int_3;
  }
}
