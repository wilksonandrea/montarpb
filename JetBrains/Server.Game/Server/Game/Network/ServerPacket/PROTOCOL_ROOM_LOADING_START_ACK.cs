// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_LOADING_START_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_LOADING_START_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 3630);
    this.WriteD(((PROTOCOL_ROOM_GET_LOBBY_USER_LIST_ACK) this).uint_0);
  }

  public PROTOCOL_ROOM_LOADING_START_ACK(uint account_1, [In] Account obj1)
  {
    ((PROTOCOL_ROOM_JOIN_ACK) this).uint_0 = account_1;
    if (obj1 == null)
      return;
    ((PROTOCOL_ROOM_JOIN_ACK) this).int_0 = obj1.SlotId;
    ((PROTOCOL_ROOM_JOIN_ACK) this).roomModel_0 = obj1.Room;
  }
}
