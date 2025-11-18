// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_CHANGE_COSTUME_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_CHANGE_COSTUME_ACK : GameServerPacket
{
  private readonly SlotModel slotModel_0;

  public PROTOCOL_ROOM_CHANGE_COSTUME_ACK(MessageModel bool_1)
  {
    ((PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK) this).messageModel_0 = bool_1;
  }

  public virtual void Write()
  {
    this.WriteH((short) 1931);
    this.WriteD((uint) ((PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK) this).messageModel_0.ObjectId);
    this.WriteQ(((PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK) this).messageModel_0.SenderId);
    this.WriteC((byte) ((PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK) this).messageModel_0.Type);
    this.WriteC((byte) ((PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK) this).messageModel_0.State);
    this.WriteC((byte) ((PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK) this).messageModel_0.DaysRemaining);
    this.WriteD(((PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK) this).messageModel_0.ClanId);
    this.WriteB(((PROTOCOL_ROOM_CHANGE_PASSWD_ACK) this).NoteClanData(((PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK) this).messageModel_0));
  }
}
