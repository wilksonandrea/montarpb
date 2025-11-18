// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_CHANGE_SLOT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_CHANGE_SLOT_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public PROTOCOL_ROOM_CHANGE_SLOT_ACK(RoomModel messageModel_1)
  {
    ((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).roomModel_0 = messageModel_1;
    if (messageModel_1 == null)
      return;
    ((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).bool_0 = messageModel_1.IsBotMode();
  }

  public PROTOCOL_ROOM_CHANGE_SLOT_ACK(RoomModel Message, [In] bool obj1)
  {
    ((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).roomModel_0 = Message;
    ((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).bool_0 = obj1;
  }
}
