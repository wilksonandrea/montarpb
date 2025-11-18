// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK : GameServerPacket
{
  private readonly RoomModel roomModel_0;
  private readonly bool bool_0;

  public virtual void Write()
  {
    this.WriteH((short) 1922);
    this.WriteD(((PROTOCOL_MESSENGER_NOTE_SEND_ACK) this).uint_0);
  }

  public PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK(SlotModel uint_1)
  {
    ((PROTOCOL_ROOM_CHANGE_COSTUME_ACK) this).slotModel_0 = uint_1;
  }

  public virtual void Write()
  {
    this.WriteH((short) 3678);
    this.WriteD(((PROTOCOL_ROOM_CHANGE_COSTUME_ACK) this).slotModel_0.Id);
    this.WriteC((byte) ((PROTOCOL_ROOM_CHANGE_COSTUME_ACK) this).slotModel_0.CostumeTeam);
  }
}
