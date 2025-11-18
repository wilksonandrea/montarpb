// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK : GameServerPacket
{
  private readonly uint uint_0;
  private readonly List<QuickstartModel> list_0;
  private readonly QuickstartModel quickstartModel_0;
  private readonly RoomModel roomModel_0;

  public virtual void Write()
  {
    this.WriteH((short) 2588);
    this.WriteD(((PROTOCOL_LOBBY_GET_ROOMLIST_ACK) this).int_3);
    this.WriteD(((PROTOCOL_LOBBY_GET_ROOMLIST_ACK) this).int_0);
    this.WriteD(((PROTOCOL_LOBBY_GET_ROOMLIST_ACK) this).int_4);
    this.WriteB(((PROTOCOL_LOBBY_GET_ROOMLIST_ACK) this).byte_0);
    this.WriteD(((PROTOCOL_LOBBY_GET_ROOMLIST_ACK) this).int_2);
    this.WriteD(((PROTOCOL_LOBBY_GET_ROOMLIST_ACK) this).int_1);
    this.WriteD(((PROTOCOL_LOBBY_GET_ROOMLIST_ACK) this).int_5);
    this.WriteB(((PROTOCOL_LOBBY_GET_ROOMLIST_ACK) this).byte_1);
  }

  public PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK([In] uint obj0)
  {
    ((PROTOCOL_LOBBY_LEAVE_ACK) this).uint_0 = obj0;
  }
}
