// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_AUTH_CHANGE_NICKNAME_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_CHANGE_NICKNAME_ACK : GameServerPacket
{
  private readonly string string_0;

  public virtual void Write()
  {
    this.WriteH((short) 6152);
    this.WriteD(((PROTOCOL_CHAR_DELETE_CHARA_ACK) this).uint_0);
    if (((PROTOCOL_CHAR_DELETE_CHARA_ACK) this).uint_0 != 0U)
      return;
    this.WriteC((byte) ((PROTOCOL_CHAR_DELETE_CHARA_ACK) this).int_0);
    this.WriteD((uint) ((PROTOCOL_CHAR_DELETE_CHARA_ACK) this).itemsModel_0.ObjectId);
    this.WriteD(((PROTOCOL_CHAR_DELETE_CHARA_ACK) this).characterModel_0.Slot);
  }
}
