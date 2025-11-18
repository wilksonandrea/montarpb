// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_CREATE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_CREATE_ACK : GameServerPacket
{
  private readonly RoomModel roomModel_0;
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 3605);
    this.WriteD(((PROTOCOL_ROOM_CHANGE_SLOT_ACK) this).uint_0);
  }

  public PROTOCOL_ROOM_CREATE_ACK(int roomModel_1, int bool_1, [In] bool obj2, [In] string obj3)
  {
    ((PROTOCOL_ROOM_CHATTING_ACK) this).int_0 = roomModel_1;
    ((PROTOCOL_ROOM_CHATTING_ACK) this).int_1 = bool_1;
    ((PROTOCOL_ROOM_CHATTING_ACK) this).bool_0 = obj2;
    ((PROTOCOL_ROOM_CHATTING_ACK) this).string_0 = obj3;
  }
}
