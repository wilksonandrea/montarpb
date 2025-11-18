// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK([In] int obj0)
  {
    ((PROTOCOL_ROOM_REQUEST_MAIN_ACK) this).uint_0 = (uint) obj0;
  }

  public virtual void Write()
  {
    this.WriteH((short) 3612);
    this.WriteD(((PROTOCOL_ROOM_REQUEST_MAIN_ACK) this).uint_0);
  }

  public PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK(uint roomModel_1)
  {
    ((PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK) this).uint_0 = roomModel_1;
  }
}
