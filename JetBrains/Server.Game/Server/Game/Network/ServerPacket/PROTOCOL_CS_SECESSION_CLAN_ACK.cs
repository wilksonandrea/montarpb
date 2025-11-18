// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_SECESSION_CLAN_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_SECESSION_CLAN_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 819);
    this.WriteD(((PROTOCOL_CS_REQUEST_LIST_ACK) this).int_0);
    if (((PROTOCOL_CS_REQUEST_LIST_ACK) this).int_0 < 0)
      return;
    this.WriteC((byte) ((PROTOCOL_CS_REQUEST_LIST_ACK) this).int_1);
    this.WriteC((byte) ((PROTOCOL_CS_REQUEST_LIST_ACK) this).int_2);
    this.WriteB(((PROTOCOL_CS_REQUEST_LIST_ACK) this).byte_0);
  }

  public PROTOCOL_CS_SECESSION_CLAN_ACK(int int_3)
  {
    ((PROTOCOL_CS_ROOM_INVITED_ACK) this).int_0 = int_3;
  }
}
