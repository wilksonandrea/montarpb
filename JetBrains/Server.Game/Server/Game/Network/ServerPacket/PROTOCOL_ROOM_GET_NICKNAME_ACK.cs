// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_GET_NICKNAME_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_GET_NICKNAME_ACK : GameServerPacket
{
  private readonly int int_0;
  private readonly string string_0;

  public PROTOCOL_ROOM_GET_NICKNAME_ACK([In] int obj0, int roomModel_1)
  {
    ((PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK) this).int_0 = obj0;
    ((PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK) this).int_1 = roomModel_1;
  }

  public virtual void Write()
  {
    this.WriteH((short) 3638);
    this.WriteD(((PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK) this).int_0);
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK) this).int_1);
  }
}
