// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_SELECT_SLOT_CHANGE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_SELECT_SLOT_CHANGE_ACK : GameServerPacket
{
  public PROTOCOL_ROOM_SELECT_SLOT_CHANGE_ACK(int roomModel_1)
  {
    ((PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK) this).uint_0 = (uint) roomModel_1;
  }

  public virtual void Write()
  {
    this.WriteH((short) 3614);
    this.WriteD(((PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK) this).uint_0);
  }
}
