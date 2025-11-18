// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_LOBBY_LEAVE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_LOBBY_LEAVE_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 2584);
    this.WriteH((short) 0);
    this.WriteD(((PROTOCOL_LOBBY_ENTER_ACK) this).uint_0);
    this.WriteC((byte) 0);
    this.WriteQ(0L);
  }

  public PROTOCOL_LOBBY_LEAVE_ACK([In] RoomModel obj0)
  {
    ((PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK) this).roomModel_0 = obj0;
  }
}
