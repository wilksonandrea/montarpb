// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_JOIN_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_JOIN_ACK : GameServerPacket
{
  private readonly uint uint_0;
  private readonly RoomModel roomModel_0;
  private readonly int int_0;

  public virtual void Write()
  {
    this.WriteH((short) 3672);
    this.WriteD(0);
    this.WriteC((byte) 68);
  }

  public PROTOCOL_ROOM_JOIN_ACK()
  {
  }

  public virtual void Write()
  {
    this.WriteH((short) 3674);
    this.WriteD(0);
    this.WriteC((byte) 68);
  }

  public PROTOCOL_ROOM_JOIN_ACK([In] uint obj0)
  {
    ((PROTOCOL_ROOM_GET_LOBBY_USER_LIST_ACK) this).uint_0 = obj0;
  }
}
